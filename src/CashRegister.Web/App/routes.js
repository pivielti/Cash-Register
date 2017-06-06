// public pages
import HomePage from './Components/Pages/HomePage.vue';
import CashRegister from './Components/Pages/CashRegister.vue';

// admin pages
import AdminRoot from './Components/Pages/Admin/AdminRootPage.vue';
import AdminDashboard from './Components/Pages/Admin/DashboardPage.vue';
import AdminCategories from './Components/Pages/Admin/CategoriesPage.vue';
import AdminProducts from './Components/Pages/Admin/ProductsPage.vue';

// auth pages
import LoginPage from './Components/Pages/LoginPage.vue';

export var routes = [
    { path: '/', name: 'index', component: HomePage },
    { path: '/cash-register', name: 'cash-register', component: CashRegister },
    {
        path: '/admin', component: AdminRoot, meta: { auth: true },
        children: [
            { path: '', name: 'admin-zone', component: AdminDashboard },
            { path: 'categories', name: 'categories-admin', component: AdminCategories },
            { path: 'products', name: 'products-admin', component: AdminProducts }
        ]
    },
    { path: '/login', name: 'login', component: LoginPage, meta: { auth: false } }
];
