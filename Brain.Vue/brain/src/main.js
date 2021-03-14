import Vue from 'vue';
import App from './App.vue';
import 'bootstrap'; 
import 'bootstrap/dist/css/bootstrap.min.css';
import VueRouter from 'vue-router'
import Home from './pages/Home.vue'
import Profile from './pages/Profile.vue'
import Login from './pages/Login.vue'
import Register from './pages/Register.vue'
import axios from 'axios'
import VueAxios from 'vue-axios'

Vue.use(VueRouter)
Vue.use(VueAxios, axios)

// App routes and components mapping.
const routes = [
  { path: '/', component: Home },
  { path: '/profile', component: Profile },
  { path: '/login', component: Login },
  { path: '/register', component: Register},
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
