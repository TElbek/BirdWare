import { fileURLToPath, URL } from 'node:url'

import { VitePWA } from 'vite-plugin-pwa'
import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import tailwindcss from '@tailwindcss/vite'

// https://vite.dev/config/
export default defineConfig({
  plugins: [
    vue(),
    VitePWA({
      strategies: 'generateSW',
      registerType: 'autoUpdate',
      includeAssets: ['favicon.ico', 'apple-touch-icon.png', 'masked-icon.svg'],
      workbox: {
        runtimeCaching: [
          {
            urlPattern: ({ url }) => url.pathname.startsWith('/api/ankomstdato'),
            handler: "NetworkFirst",
            method: 'GET',
            options: {
              cacheName: "api-cache",
            }
          },
          {
            urlPattern: ({ url }) => url.pathname.startsWith('/api/arter'),
            handler: "NetworkFirst",
            method: 'GET',
            options: {
              cacheName: "api-cache",
            }
          },
          {
            urlPattern: ({ url }) => url.pathname.startsWith('/api/forskel'),
            handler: "NetworkFirst",
            method: 'GET',
            options: {
              cacheName: "api-cache",
            }
          },
          {
            urlPattern: ({ url }) => url.pathname.startsWith('/api/hvorkanjegfinde'),
            handler: "NetworkFirst",
            method: 'GET',
            options: {
              cacheName: "api-cache",
            }
          },
          {
            urlPattern: ({ url }) => url.pathname.startsWith('/api/aaretsgang'),
            handler: "NetworkFirst",
            method: 'GET',
            options: {
              cacheName: "api-cache",
            }
          },
          {
            urlPattern: ({ url }) => url.pathname.startsWith('/api/fugletur/seneste/observationer'),
            handler: "NetworkFirst",
            method: 'GET',
            options: {
              cacheName: "api-cache",
            }
          },
          {
            urlPattern: /^https:\/\/birdware\.dk\/api\/observation\/opretobs\//,
            handler: "NetworkOnly",
            method: 'POST',
            options: {
              backgroundSync: {
                name: 'api-post-queue',
                options: {
                  maxRetentionTime: 24 * 60,
                }
              }
            }
          }]
      },
      devOptions: {
        enabled: true
      },
      manifest: {
        name: 'Birdware PWA',
        short_name: 'BirdwarePWA',
        description: 'Birdware PWA with Vue and Vite',
        theme_color: '#ffffff',
        icons: [
          {
            src: 'favicon.ico',
            sizes: '192x192',
            type: 'image/x-icon'
          }
        ]
      }
    }),
    tailwindcss(),
  ],
  server: {
    port: 8080
  },
  resolve: {
    alias: {
      '@': fileURLToPath(new URL('./src', import.meta.url))
    },
  },
})
