<script setup>
import { ref } from 'vue'

const email = ref('')
const password = ref('')
const loading = ref(false)
const error = ref('')

async function login() {

  error.value = ''

  if(!email.value || !password.value){
    error.value = "Por favor ingresa tu correo y contraseña"
    return
  }

  try{

    loading.value = true

    // aquí luego conectas tu API
    await new Promise(r => setTimeout(r,1000))

    console.log("Login:", email.value, password.value)

  }
  catch(e){
    error.value = "Invalid credentials"
  }
  finally{
    loading.value = false
  }

}
</script>

<template>

<div class="login-page">

  <div class="login-card">

    <div class="login-left">

        <!-- Logo -->
       <img
           src="../assets/vm-logo-horizontal.png"
           alt="Logo" 
           class="logo"
        >

      <p class="subtitle">
        Sistema de gestión de vacaciones de empleados
      </p>


    </div>

    <div class="login-right">

      <h2 class="title">
        Iniciar sesión
      </h2>

      <p class="description">
        Ingresa tus credenciales para acceder a la plataforma
      </p>

      <form class="login-form" @submit.prevent="login">

        <div class="form-group">
            <label class="label">Correo</label>
          <input
            class="input"
            v-model="email"
            type="email"
            placeholder="correo@company.com"
          >
        </div>

        <div class="form-group">
            <label class="label">Contraseña</label>

          <input
            class="input"
            v-model="password"
            type="password"
            placeholder="••••••••"
          >
        </div>

        <div v-if="error" class="error">
          {{ error }}
        </div>

        <button
          class="btn btn-primary w-full shine-effect"
          :disabled="loading"
        >

          <span v-if="loading">
            Iniciando sesión...
          </span>

          <span v-else>
            Iniciar sesión
          </span>

        </button>

      </form>

    </div>

  </div>

</div>

</template>

<style scoped>

.form-group{

  display:flex;
  flex-direction:column;

  gap:6px;

  width:100%;

}

.label{

  font-size:14px;
  font-weight:500;

  color:var(--text);

}
.login-page{

  height:100vh;

  display:flex;
  align-items:center;
  justify-content:center;

}


.login-card{

  display:flex;

  width:900px;
  height:520px;

  background:white;

  border-radius:16px;

  overflow:hidden;

  box-shadow:0 10px 40px rgba(0,0,0,0.1);

}

/* left */

.login-left{

  flex:1;

  background:linear-gradient(135deg, var(--primary),var(--info));

  color:white;

  display:flex;
  flex-direction:column;

  justify-content:center;
  align-items:center;

  padding:40px;

  text-align:center;

}

.brand{

  font-size:36px;
  font-weight:700;

}

.subtitle{

  margin-top:10px;
  opacity:.85;

}

.login-illustration{

  font-size:80px;

  margin-top:40px;

}

/* right */

.login-right{

  flex:1;

  padding:50px;

  display:flex;
  flex-direction:column;
  justify-content:center;

}

.title{

  font-size:28px;
  margin-bottom:5px;
  color: var(--primary-hover)

}

.description{

  color:#6b7280;

  margin-bottom:30px;

}

.login-form{

  display:flex;
  flex-direction:column;

  gap:20px;

}


.error{

  background:#fee2e2;

  color:#b91c1c;

  padding:10px;

  border-radius:6px;

  font-size:13px;

}
.logo {
  height: 100px;
  width: auto;
  cursor: pointer;
  transition: transform 0.2s ease;
}

</style>