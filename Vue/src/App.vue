<template>
  <ContactPage />
</template>

<script>
import ContactPage from "./components/ContactPage.vue";

export default {
  name: "App",
  components: {
    ContactPage,
  },
};
</script>

<style>
#app {
  font-family: Helvetica, Arial, sans-serif;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
