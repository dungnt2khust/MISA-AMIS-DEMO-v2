import EmployeePage from '../components/views/employee/EmployeePage'
import CustomerPage from '../components/views/customer/CustomerPage'
import VueRouter from 'vue-router'
import Vue from 'vue'

Vue.use(VueRouter);

const routes = [
    {path: '/', redirect: '/employees'},
    {path: '/employees', component: EmployeePage},
    {path: '/customers', component: CustomerPage}
];

const router = new VueRouter({
    routes: routes,
    mode: 'history'
});

export default router;