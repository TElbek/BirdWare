import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap';

import 'vue-multiselect/dist/vue-multiselect.css';
import '@/assets/scss/base.scss';

import { createApp, defineAsyncComponent } from 'vue'
import { createPinia } from 'pinia'

import App from './App.vue'
import router from './router'

const app = createApp(App)

registerComponents();
registerBootstrapComponents();

app.use(createPinia())
app.use(router)

app.mount('#app');

function registerComponents() {
    app.component('tableBirdware', defineAsyncComponent(() => import('@/components/main/table/table.vue')));
    app.component('tableRowBirdware', defineAsyncComponent(() => import('@/components/main/table/tablerow.vue')));
    app.component('tableCellBirdware', defineAsyncComponent(() => import('@/components/main/table/tablecell.vue')));
}

function registerBootstrapComponents() {
    app.component('bsCard', defineAsyncComponent(() => import('@/components/main/bootstrap/bscard.vue')));
    app.component('bsCardHeader', defineAsyncComponent(() => import('@/components/main/bootstrap/bscardheader.vue')));
    app.component('bsCardBody', defineAsyncComponent(() => import('@/components/main/bootstrap/bscardbody.vue')));
    app.component('bsRowCols', defineAsyncComponent(() => import('@/components/main/bootstrap/bsrowcols.vue')));
}