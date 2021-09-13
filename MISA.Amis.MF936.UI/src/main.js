import Vue from 'vue'
import App from './App.vue'
import router from './Routers/index'
import ResourcesVN from './Resources/resourcesVN'

Vue.config.productionTip = false;

// ResoucesVN
Vue.prototype.$resourcesVN = ResourcesVN;

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

