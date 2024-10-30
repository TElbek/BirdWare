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
        title: 'Hjem',
        showInNavBar: true
      }
    },
    {
      path: '/forskel',
      name: 'forskel',
      component: () => import('../views/ForskelView.vue'),
      meta: {
        title: 'Forskel',
        showInNavBar: true
      }
    },
    {
      path: '/fugletur',
      name: 'fugletur',
      component: () => import('../components/fugletur/fugletur.vue'),
      redirect: {name: 'fugletur-obs'},
      meta: {
        title: 'Fugletur',
        showInNavBar: true
      },
      children: [
        {
          path: '/fugletur/observationer',
          name: 'fugletur-obs',
          component: () => import('../components/fugletur/fugletur-obs.vue'),
          meta: {
            title: 'Observation',
            showInNavBar: false
          },
        },
        {
          path: '/fugletur/analyse',
          name: 'fugletur-analyse',
          component: () => import('../components/fugletur/fugletur-analyse.vue'),
          meta: {
            title: 'Analyse',
            showInNavBar: false
          },
        },
        {
          path: '/fugletur/oversigt',
          name: 'fugletur-oversigt',
          component: () => import('../components/fugletur/fugletur-oversigt.vue'),
          meta: {
            title: 'Fugleture - Oversigt',
            showInNavBar: false
          },
        }
      ]
    },
    {
      path: '/art/:id/observation/',
      name: 'art-observation-id',
      component: () => import('../views/ObservationView.vue'),
      meta: {
        title: 'Observation',
        showInNavBar: false
      }
    },
    {
      path: '/art/observation/',
      name: 'art-observation',
      component: () => import('../views/ObservationView.vue'),
      meta: {
        title: 'Observation',
        showInNavBar: true
      }
    },
    {
      path: '/aaretsgang',
      name: 'aaretsgang',
      component: () => import('../views/AaretsGangView.vue'),
      meta: {
        title: 'Årets Gang',
        showInNavBar: true
      }
    }
  ]
})

export default router