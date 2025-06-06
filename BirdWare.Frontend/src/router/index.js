import { createRouter, createWebHashHistory } from 'vue-router'
import { useAuthenticateStore } from '@/stores/authenticate.js';
import HomeView from '../views/HomeView.vue'

const router = createRouter({
  history: createWebHashHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView,
      meta: {
        title: 'Hjem',
        showInNavBar: false,
        requireSSL: false
      }
    },
    {
      path: '/fugletur',
      name: 'fugletur',
      component: () => import('../components/fugletur/fugletur.vue'),
      redirect: { name: 'fugletur-obs' },
      meta: {
        title: 'Fugletur',
        showInNavBar: true,
        requireSSL: false
      },
      children: [
        {
          path: '/fugletur/observationer',
          name: 'fugletur-obs',
          component: () => import('../components/fugletur/fugletur-obs.vue'),
          meta: {
            title: 'Fugletur',
            showInNavBar: false,
            requireSSL: false,
            requireId: true
          },
        },
        {
          path: '/fugletur/analyse',
          name: 'fugletur-analyse',
          component: () => import('../components/fugletur/fugletur-analyse.vue'),
          meta: {
            title: 'Analyse',
            showInNavBar: false,
            requireSSL: false,
            requireId: true
          },
        },
        {
          path: '/fugletur/oversigt',
          name: 'fugletur-oversigt',
          component: () => import('../components/fugletur/fugletur-oversigt.vue'),
          meta: {
            title: 'Oversigt',
            showInNavBar: false,
            requireSSL: false,
            requireId: false
          },
        }
      ]
    },
    {
      path: '/art/observation/',
      name: 'art-observation',
      component: () => import('../views/ObservationView.vue'),
      meta: {
        title: 'Observation',
        showInNavBar: true,
        requireSSL: false
      },      
    },
    {
      path: '/forskel',
      name: 'forskel',
      component: () => import('../views/ForskelView.vue'),
      meta: {
        title: 'Forskel',
        showInNavBar: true,
        requireSSL: false
      }
    },
    {
        path: '/statistik/hvorkanjegfinde',
        name: 'hvorkanjegfinde',
        component: () => import('../views/HvorKanJegFinde.vue'),
        meta: {
          title: 'Hvor kan Jeg finde',
          showInNavBar: true,
          requireSSL: false
        },      
      },
    {
      path: '/opret/oprettur',
      name: 'addtrip',
      component: () => import('../views/OpretTurView.vue'),
      meta: {
        title: 'Tilføj Tur',
        showInNavBar: false,
        requiresAuth: true,
        requireSSL: true
      }
    },
    {
      path: '/opret/opretobs',
      name: 'addobs',
      component: () => import('../views/OpretObsView.vue'),
      redirect: { name: 'addobs-liste' },
      meta: {
        title: 'Tilføj Observation',
        showInNavBar: false,
        requiresAuth: true,
        requireSSL: true
      },
      children: [
        {
          path: '/opret/opretobs/forslag',
          name: 'addobs-forslag',
          component: () => import('../components/opret/opretobs-forslag.vue'),
          meta: {
            title: 'Tilføj Observation',
            showInNavBar: false,
            requiresAuth: true,
            requireSSL: true
          }
        },
        {
          path: '/opret/opretobs/liste',
          name: 'addobs-liste',
          component: () => import('../components/opret/opretobs-liste.vue'),
          meta: {
            title: 'Tilføj Observation',
            showInNavBar: false,
            requiresAuth: true,
            requireSSL: true
          }
        },
      ]
    },
    {
      path: '/aaretsgang',
      name: 'aaretsgang',
      component: () => import('../views/AaretsGangView.vue'),
      meta: {
        title: 'Årets Gang',
        showInNavBar: true,
        requireSSL: false
      }
    },
    {
      path: '/login',
      name: 'login',
      component: () => import('../views/Login.vue'),
      meta: {
        title: 'Login',
        showInNavBar: false,
        requireSSL: true
      }
    }
  ]
})

router.beforeEach((to, from, next) => {
  if (to.meta.title != undefined && to.meta.title.length > 0) {
    document.title = to.meta.title + ' - Birdware.dk';
  }
  else {
    document.title = 'Birdware.dk';
  }

  const authenticate = useAuthenticateStore();

  if (to.matched.some(record => record.meta.requiresAuth)) {
    let authenticated = authenticate.isLoggedIn;
    if (!authenticated) {
      next({
        path: '/login',
        query: { redirect: to.fullPath }
      })
    } else {
        next();
    }
  } else {
    next();
  }
});

export default router