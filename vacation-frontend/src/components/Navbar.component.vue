<template>
  <nav class="navbar">
    <div class="nav-menu">

      <img
        src="../assets/vm-logo-horizontal.png"
        alt="Logo"
        class="logo"
        @click="router.push('/dashboard')"
      >

      <div
        v-for="module in modules"
        :key="module.name"
        class="nav-item"
        :class="{
          'has-dropdown': module.dropdown,
          'nav-item-active': isParentActive(module)
        }"
      >
        <!-- item normal -->
        <RouterLink
          v-if="!module.dropdown"
          :to="module.route"
          class="nav-link"
          exact-active-class="nav-link-exact-active"
        >
          {{ module.name }}
        </RouterLink>

        <!-- item con dropdown -->
        <div
          v-else
          class="nav-link"
          :class="{ 'nav-link-exact-active': isParentActive(module) }"
        >
          {{ module.name }}

          <div class="dropdown-menu">
            <RouterLink
              v-for="child in module.children"
              :key="child.name"
              :to="child.route"
              class="dropdown-item"
              active-class="dropdown-item-active"
              exact-active-class="dropdown-item-exact-active"
            >
              {{ child.name }}
            </RouterLink>
          </div>
        </div>
      </div>

    </div>
  </nav>
</template>

<script setup>
import { ref } from 'vue'
import { RouterLink, useRouter, useRoute } from 'vue-router'

const router = useRouter()
const route = useRoute()

const modules = ref([
  {
    name: "Dashboard",
    route: "/dashboard",
    dropdown: false
  },
  {
    name: "Solicitud Vacaciones",
    route: "/leaves",
    dropdown: false
  },
// TODO: realizar varios reportes, como por ejemplo: vacaciones por departamento, empleados con menos días disponibles, etc
//   {
//       name: "Reportes",
//       route: "/reports",
//       dropdown: false
//     },
    {
      name: "Configuración",
      dropdown: true,
      children: [
        { name: "Empleados", route: "/config/employees" },
        { name: "Departamentos", route: "/config/departments" },
        { name: "Días Extra", route: "/config/extra-days" },
        { name: "Feriados", route: "/config/holidays" },]
    },
  {
    name: "Administración",
    dropdown: true,
    children: [
      { name: "Usuarios", route: "/admin/users" },
      { name: "Roles", route: "/admin/roles" },
      { name: "Auditoría", route: "/admin/audit" },
    ]
  }
])
function isParentActive(module) {
  if (!module.dropdown) return false
  return module.children.some(child => route.path.startsWith(child.route))
}

</script>
<style scoped>

.logo {
  height: 40px;
  width: auto;
  cursor: pointer;
  margin-right: 40px;
  transition: transform 0.2s ease;
}

.logo:hover {
  transform: scale(1.05);
}

</style>
