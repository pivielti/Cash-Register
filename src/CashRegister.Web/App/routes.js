import HomePage from './Components/Pages/HomePage.vue';
import CashRegister from './Components/Pages/CashRegister.vue';

export var routes = [
    { path: '/', name: 'index', component: HomePage },
    { path: '/cash-register', name: 'cash-register', component: CashRegister },
];
