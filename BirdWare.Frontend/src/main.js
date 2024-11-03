import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap';
import 'vue-multiselect/dist/vue-multiselect.css';

import artNavn from '@/components/main/artnavn.vue';
import fugleturDato from './components/main/fugleturdato.vue';

import { createApp } from 'vue'
import { createPinia } from 'pinia'

import App from './App.vue'
import router from './router'

const app = createApp(App)

app.use(createPinia())
app.use(router)

app
    .component('artNavn', artNavn)
    .component('fugleturDato', fugleturDato);

app.mount('#app')
