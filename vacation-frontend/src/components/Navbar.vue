<template>

<nav class="navbar">

  <div class="nav-menu">

    <!-- Logo -->
    <img
     src="../assets/vm-logo-horizontal.png"
     alt="Logo" 
     class="logo"
     @click="router.push('/')"
     >

    <div
  v-for="module in modules"
  :key="module.name"
  class="nav-item"
  :class="{ 'has-dropdown': module.dropdown }"
>
  <div
    v-if="!module.dropdown"
    class="nav-link"
    @click="router.push(module.route)"
  >
    {{ module.name }}
  </div>

  <div
    v-else
    class="nav-link"
  >
    {{ module.name }}

    <div class="dropdown-menu">
      <div
        v-for="child in module.children"
        :key="child.name"
        class="dropdown-item"
        @click="router.push(child.route)"
      >
        {{ child.name }}
      </div>
    </div>
  </div>
</div>

  </div>

</nav>

</template>

<script setup>
import { ref } from 'vue'
import { useRouter } from 'vue-router'

const router = useRouter()

const modules = ref([
  {
    name: "Dashboard",
    route: "/",
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
</script>
<style scoped>

.logo {
  height: 40px;
  width: auto;
  cursor: pointer;
  transition: transform 0.2s ease;
}

.logo:hover {
  transform: scale(1.05);
}

</style>