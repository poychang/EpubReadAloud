navigator.serviceWorker.register('service-worker.js')
    .then(registration => {
        console.log(`Service Worker Registered (#{Build.BuildNumber}#)`);

        // detect Service Worker update available and wait for it to become installed
        registration.addEventListener('updatefound', () => {
            if (!registration.installing) {
                return;
            }

            // wait until the new Service worker is actually installed (ready to take over)
            registration.installing.addEventListener('statechange', () => {
                if (!registration.waiting) {
                    return;
                }

                // if there's an existing controller (previous Service Worker), show the prompt
                if (navigator.serviceWorker.controller) {
                    invokeServiceWorkerUpdateFlow(registration);
                } else {
                    // otherwise it's the first install, nothing to do
                    console.log('Service Worker initialized for the first time');
                }
            });
        });

        let refreshing = false;

        // detect controller change and refresh the page
        navigator.serviceWorker.addEventListener('controllerchange', () => {
            if (!refreshing) {
                window.location.reload();
                refreshing = true;
            }
        });
    });

function invokeServiceWorkerUpdateFlow(registration) {
    if (registration.waiting) {
        // let waiting Service Worker know it should became active
        registration.waiting.postMessage('SKIP_WAITING')
    }
}