import { createRouter, createWebHistory } from 'vue-router'
import DashboardPage from '@/pages/DashboardPage.vue'
import LeavesPage from '@/pages/LeavesPage.vue'
import LoginPage from '@/auth/LoginPage.vue'
import MainLayout from '@/pages/MainLayout.vue'
import EmployeePage from '@/pages/settings/EmployeePage.vue'
import RolePage from '@/pages/admin/RolePage.vue'
import DepartmentPage from '@/pages/settings/DepartmentPage.vue'
import ExtraBenefitDayPage from '@/pages/settings/ExtraBenefitDayPage.vue'
import HolidayPage from '@/pages/settings/HolidayPage.vue'
import UsersPage from '@/pages/admin/UsersPage.vue'
import AuditPage from '@/pages/admin/AuditPage.vue'

export const router = createRouter({
  history: createWebHistory(),
  routes: [
    { path: '/',
       component: MainLayout,
       children: [
        { path: '/dashboard', name: 'dashboard', component: DashboardPage } ,
        { path: '/leaves', name: 'leaves', component: LeavesPage },
        { path: '/employees', name: 'employees', component: EmployeePage },
        /*Settings module*/
        { path: '/config/employees', name: 'config-employees', component: EmployeePage},
        { path: '/config/departments', name: 'config-departments', component: DepartmentPage},
        { path: '/config/extra-days', name: 'config-extra-days', component: ExtraBenefitDayPage },
        { path: '/config/holidays', name: 'config-holidays', component: HolidayPage },
        /* Admin module */
        { path: 'admin/users', name: 'admin-users', component: UsersPage },
        { path: 'admin/roles', name: 'admin-roles', component: RolePage },
        { path: 'admin/audit', name: 'admin-audit', component: AuditPage },

       ]
    },
    {
      path: '/login',
      name: 'login',
      component: LoginPage
    }
  ],
})
