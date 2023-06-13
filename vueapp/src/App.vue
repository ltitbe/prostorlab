<template>
  <img alt="Vue logo" src="./assets/logo.png">
  <UserTable/>
</template>

<script>
import UserTable from './components/UserTable.vue'

export default {
  name: 'App',
  components: {
      UserTable
  }
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
