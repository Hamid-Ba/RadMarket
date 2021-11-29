////const STATIC_CACHE_VERSION = 'static_1'
////const DYNAMIC_CACHE_VERSION = 'dynamic_1'

////self.addEventListener('install', function (event) {
////    console.log('[SW] installing', event);
////});

////self.addEventListener('activate', function (event) {
////    console.log('[SW] activating', event);
////});


////function cleanup() {
////    caches.keys()
////        .then((keys) => {
////            return Promise.all(keys.map((key) => {
////                if (key !== STATIC_CACHE_VERSION && key != DYNAMIC_CACHE_VERSION) {
////                    console.log('[SW] Remove Old Cache ', key);
////                    return caches.delete(key);
////                }
////            }));
////        })
////}
////function preCache() {
////    caches.open(STATIC_CACHE_VERSION)
////        .then((cache) => {
////            console.log('cache ready');
////            return cache.addAll(STATIC_ASSESTS);
////        })
////        .catch(e => {
////            console.log('cache error');
////        })
////}

////self.addEventListener('fetch', (event) => {
////    console.log('[SW] fetching ...');
////    const request = event.request;

////    event.respondWith(
////        caches.match(request)
////            .then((response) => {
////                return response || fetch(request)
////                    .then((res) => {
////                        caches.open(DYNAMIC_CACHE_VERSION)
////                            .then((cache) => {
////                                cache.put(request, res);
////                            });
////                        return res.clone();
////                    })
////                    .catch((err) => {
////                        console.log('[SW] cache fetch error');
////                        return caches.open(STATIC_CACHE_VERSION)
////                            .then(function (cache) {
////                                if (request.headers.get('accept').includes('text/html')) {
////                                    return cache.match('/offline.html');
////                                }
////                                if (request.url.match(/\.(jpe?g|png|gif|svg)$/)) {
////                                    return cache.match('/images/placeholder.png');
////                                }
////                            });
////                    });
////            })
////            .catch(console.error)
////    );
////    // Network Only
////    event.respondWith(
////        fetch(event.request)
////    );
////    event.respondWith(
////        fetch(request)
////            .then((res) => {
////                caches.open(DYNAMIC_CACHE_VERSION)
////                    .then(cache => cache.put(request, res));
////                return res.clone();
////            })
////            .catch(err => caches.match(request))
////    );
////});