import Vue from 'vue'
import App from './App.vue'
import router from './routers/index'
import ResourcesVN from './resources/resourcesVN'
import Api from './mixins/apis/api'

Vue.config.productionTip = false;

// ResoucesVN
Vue.prototype.$resourcesVN = ResourcesVN;

// API
Vue.prototype.$api = Api;

// EVENT BUS
const EventBus = new Vue();
Object.defineProperties(Vue.prototype, {
  $bus: {
    get: function() {
      return EventBus;
    }
  }
});

new Vue({
  router,
  render: h => h(App),
}).$mount('#app');

