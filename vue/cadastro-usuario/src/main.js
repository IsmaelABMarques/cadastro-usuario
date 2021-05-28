import Vue from 'vue'
import App from './App.vue'
import router from './router/'
import store from './store/'
import vuetify from './plugins/vuetify'
import SLoading from '@signainfo/sloading'
import '@signainfo/sloading/dist/sloading.css'
import SToast from '@signainfo/stoast'
import '@signainfo/stoast/dist/stoast.css'
import SAutoComplete from '@signainfo/sautocomplete'
import '@signainfo/sautocomplete/dist/sautocomplete.css'

Vue.config.productionTip = false

Vue.use(SLoading)
Vue.use(SToast)
Vue.use(SAutoComplete)

new Vue({
  router,
  store,
  vuetify,
  render: h => h(App)
}).$mount('#app')
