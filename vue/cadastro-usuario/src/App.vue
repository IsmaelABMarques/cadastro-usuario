<template>
  <v-app :style="bcg">
    <Home />
    <s-loading v-if="loading.show" :show="loading.show" :message="loading.message" />
    <s-toast v-model="showToast" :color="toast.color" :message="toast.message" :time="toast.time" />
  </v-app>
</template>

<script>
import { mapGetters, mapActions } from 'vuex'
import Home from './views/Home'
export default {
  name: 'App',
  components: {
    Home
  },
  data() {
    return {
      bcg: { backgroundColor: localStorage.getItem('corFundo') }
    }
  },
  computed: {
    showToast: {
      get: function() {
        return this.toast.show
      },
      set: function(value) {
        this.setToast({ show: value, message: '', color: 'success', time: 3000 })
      }
    },
    ...mapGetters(['loading', 'toast'])
  },
  methods: {
    ...mapActions(['setToast'])
  }
}
</script>

<style>
.font-xs {
  font-size: 12px !important;
}
.font-sm {
  font-size: 14px !important;
}
.font-md {
  font-size: 16px !important;
}
.font-lg {
  font-size: 18px !important;
}
.font-xl {
  font-size: 20px !important;
}
.font-xxl {
  font-size: 28px !important;
}
.font-xxxl {
  font-size: 38px !important;
}
.font-xxxl {
  font-size: 46px !important;
}
.v-btn {
  text-transform: none !important;
}
div.v-input--dense {
  font-size: 14px !important;
}
</style>
