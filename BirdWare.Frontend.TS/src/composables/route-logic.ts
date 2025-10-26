import { computed } from 'vue';
import { useRouter, useRoute } from 'vue-router';

export function useRouteLogic() {
    const router = useRouter();
    const route = useRoute();

    const homeRoute = router.options.routes.find(route => route.path === '/');

    const isAtHomeRoute = computed(() => {
        return route.path === '/';
    });

    const visibleRoutes = computed(() => {
        return router.options.routes.filter((route) =>
            route.meta?.showInNavBar == true &&
            (route.meta?.requireSSL == true &&
                location.protocol == 'https:' ||
                location.hostname == 'localhost' ||
                route.meta.requireSSL == false))
    });

    return {
        isAtHomeRoute,
        visibleRoutes,
        homeRoute
    };
}