import { computed } from 'vue';
import { useRouter, useRoute, type RouteRecordRaw } from 'vue-router';

export function useRouteLogic() {
    const router = useRouter();
    const route = useRoute();

    const homeRoute = router.options.routes.find(route => route.path === '/');
    const obsRoute =  router.options.routes.find(route => route.name === 'observation');
    const turRoute = findFugleturObsRoute();

    function findFugleturObsRoute(): RouteRecordRaw | undefined {
        let fugleturRoute = router.options.routes.find(route => route.name === 'fugletur');
        if (!fugleturRoute || !fugleturRoute.children) {
            return undefined;
        }
        return fugleturRoute.children.find(child => child.name === 'fugletur-obs');        
    }

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
        homeRoute,
        obsRoute,
        turRoute
    };
}