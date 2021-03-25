import Vue from 'vue';
import { BootstrapVue, IconsPlugin } from 'bootstrap-vue'
import App from './App.vue';
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'
import VueRouter from 'vue-router';
import Home from './pages/Home.vue';
import Profile from './pages/Profile.vue';
import Login from './pages/Login.vue';
import Register from './pages/Register.vue';
import axios from 'axios';
import VueAxios from 'vue-axios';
import VueCookies from 'vue-cookies';
import Logout from "@/components/auth/Logout";

Vue.config.productionTip = false

Vue.use(VueCookies)
Vue.use(VueRouter)
Vue.use(VueAxios, axios)

Vue.use(BootstrapVue)
Vue.use(IconsPlugin)

// App routes and components mapping.
const routes = [
  { name: 'Home', path: '/', component: Home },
  { name: 'Profile', path: '/profile', component: Profile,      
    beforeEnter: (to, from, next) => {
      // Protect the profile route from anonymous users.
      let token = localStorage.getItem('brain_token');
      if (token !== null) {
        next();
      }
      next(false);
    }},
  { name: 'Login', path: '/login', component: Login },
  { name: 'Logout', path: '/logout', component: Logout},
  { name: 'Register', path: '/register', component: Register},
]

// Configure roter options.
const router = new VueRouter({
  mode: 'history',
  routes // short for `routes: routes`
})

Vue.config.productionTip = false

new Vue({
  router,
  render: h => h(App),
}).$mount('#app')
