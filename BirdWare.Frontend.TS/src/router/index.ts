import { createRouter, createWebHashHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'

const router = createRouter({
  history: createWebHashHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView,
      meta: {
        showInNavBar: false,
        title: 'Hjem',
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
        showInNavBar: true,
        title: 'Forskel',
        requireSSL: false
      }
    },
    {
      path: '/hvorkanjegfinde',
      name: 'hvorkanjegfinde',
      component: () => import('../views/HvorKanJegFindeView.vue'),
      meta: {
        showInNavBar: true,
        title: 'Hvor kan jeg finde',
        requireSSL: false
      }
    },
    {
      path: '/aaretsgang',
      name: 'aaretsgang',
      component: () => import('../views/AaretsGangView.vue'),
      meta: {
        showInNavBar: true,
        title: 'Årets gang',
        requireSSL: false
      }
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
  ],
})

export default router
