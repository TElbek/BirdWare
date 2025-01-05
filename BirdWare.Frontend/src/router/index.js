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
        showInNavBar: true,
        requireSSL: false
      }
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
      path: '/fugletur',
      name: 'fugletur',
      component: () => import('../components/fugletur/fugletur.vue'),
      redirect: {name: 'fugletur-obs'},
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
            title: 'Observation',
            showInNavBar: false,
            requireSSL: false
          },
        },
        {
          path: '/fugletur/analyse',
          name: 'fugletur-analyse',
          component: () => import('../components/fugletur/fugletur-analyse.vue'),
          meta: {
            title: 'Analyse',
            showInNavBar: false,
            requireSSL: false
          },
        },
        {
          path: '/fugletur/oversigt',
          name: 'fugletur-oversigt',
          component: () => import('../components/fugletur/fugletur-oversigt.vue'),
          meta: {
            title: 'Fugleture - Oversigt',
            showInNavBar: false,
            requireSSL: false
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
      }
    },
    {
      path: '/opret/oprettur',
      name: 'addtrip',
      component: () => import('../views/OpretTurView.vue'),
      meta: {
        title: 'Tilføj Tur',
        showInNavBar: true,
        requireSSL: true
      }
    },
    {
      path: '/opret/opretobs',
      name: 'addtrip',
      component: () => import('../views/OpretObsView.vue'),
      redirect: {name: 'addobs-forslag'},
      meta: {
        title: 'Tilføj Observation',
        showInNavBar: false,
        requireSSL: true
      },
      children: [
        {
          path: '/opret/opretobs/forslag',
          name: 'addobs-forslag',
          component: () => import('../components/opret/opretobs-forslag.vue'),
          meta: {
            title: 'Forslag',
            showInNavBar: false,
            requireSSL: true
          }
        },            
        {
          path: '/opret/opretobs/liste',
          name: 'addobs-liste',
          component: () => import('../components/opret/opretobs-liste.vue'),
          meta: {
            title: 'Liste',
            showInNavBar: false,
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
    }
  ]
})

export default router