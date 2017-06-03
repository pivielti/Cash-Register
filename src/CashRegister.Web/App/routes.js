import HomePage from './Components/Pages/HomePage.vue';
import CashRegister from './Components/Pages/CashRegister.vue';
import AdminPage from './Components/Pages/AdminPage.vue';
import LoginPage from './Components/Pages/LoginPage.vue';

export var routes = [
    { path: '/', name: 'index', component: HomePage },
    { path: '/cash-register', name: 'cash-register', component: CashRegister },
    { path: '/admin', name: 'admin-zone', component: AdminPage, meta: { auth: true } },
    { path: '/login', name: 'login', component: LoginPage, meta: { auth: false } }
];
