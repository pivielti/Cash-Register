// public pages
import HomePage from './Components/Pages/HomePage.vue';
import CashRegister from './Components/Pages/CashRegister.vue';

// admin pages
import AdminRoot from './Components/Pages/Admin/AdminRootPage.vue';
import AdminDashboard from './Components/Pages/Admin/DashboardPage.vue';
import AdminCategories from './Components/Pages/Admin/CategoriesPage.vue';
import AdminCategoryEdit from './Components/Pages/Admin/Categories/CategoryEdit.vue';
import AdminProducts from './Components/Pages/Admin/ProductsPage.vue';
import AdminProductEdit from './Components/Pages/Admin/Products/ProductEdit.vue';

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
            { path: 'categories/create', name: 'categories-admin-create', component: AdminCategoryEdit, meta: { mode: "CREATE" }, props: true },
            { path: 'categories/edit/:id', name: 'categories-admin-edit', component: AdminCategoryEdit, meta: { mode: "EDIT" }, props: true },
            { path: 'products', name: 'products-admin', component: AdminProducts },
            { path: 'products/create', name: 'products-admin-create', component: AdminProductEdit, meta: { mode: "CREATE" }, props: true  },
            { path: 'products/edit/:id', name: 'products-admin-edit', component: AdminProductEdit, meta: { mode: "EDIT" }, props: true  }
        ]
    },
    { path: '/login', name: 'login', component: LoginPage, meta: { auth: false } }
];
