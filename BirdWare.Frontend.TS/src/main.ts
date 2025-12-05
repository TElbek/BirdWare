import 'vue-multiselect/dist/vue-multiselect.css';
import '@/assets/css/main.css';

import { createApp, defineAsyncComponent } from 'vue'
import { createPinia } from 'pinia'

import App from './App.vue'
import router from './router'
// import {Icon} from 'leaflet';

// Icon.Default.mergeOptions({
//    iconUrl:   require('leaflet/dist/images/marker-icon.png'),
//    shadowUrl: require('leaflet/dist/images/marker-shadow.png'),
// });

// function require(imgName: string): string {
//     return new URL(`${imgName}`, import.meta.url).href;
// }

const app = createApp(App)

registerComponents();
registerTailWindComponents();

app.use(createPinia())
app.use(router)

app.mount('#app');

function registerComponents() {
    app.component('art-navn', defineAsyncComponent(() => import('./components/main/artNavn.vue')));
    app.component('show-more-ui', defineAsyncComponent(() => import('./components/main/show-more-ui.vue')));
}

function registerTailWindComponents() {
    app.component('tw-button', defineAsyncComponent(() => import('./components/main/tailwind/tw-button.vue')));
    app.component('tw-button-dropdown', defineAsyncComponent(() => import('./components/main/tailwind/tw-button-dropdown.vue')));
    app.component('tw-button-responsive', defineAsyncComponent(() => import('./components/main/tailwind/tw-button-responsive.vue')));
    app.component('tw-button-group', defineAsyncComponent(() => import('./components/main/tailwind/tw-button-group.vue')));
    app.component('tw-card', defineAsyncComponent(() => import('./components/main/tailwind/tw-card.vue')));
    app.component('tw-card-header', defineAsyncComponent(() => import('./components/main/tailwind/tw-card-header.vue')));
    app.component('tw-flex', defineAsyncComponent(() => import('./components/main/tailwind/tw-flex.vue')));
    app.component('tw-grid-cols-five', defineAsyncComponent(() => import('./components/main/tailwind/tw-grid-cols-five.vue')));
    app.component('tw-grid-cols-three', defineAsyncComponent(() => import('./components/main/tailwind/tw-grid-cols-three.vue')));
    app.component('tw-show-lg', defineAsyncComponent(() => import('./components/main/tailwind/tw-show-lg.vue')));
    app.component('tw-show-md', defineAsyncComponent(() => import('./components/main/tailwind/tw-show-md.vue')));
    app.component('tw-text-sizeable', defineAsyncComponent(() => import('./components/main/tailwind/tw-text-sizeable.vue')));
}
