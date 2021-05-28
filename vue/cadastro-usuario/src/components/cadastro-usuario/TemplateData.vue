<template>
  <v-menu
    ref="menu"
    v-model="menu"
    :close-on-content-click="false"
    transition="scale-transition"
    offset-y
    max-width="290px"
    min-width="290px"
  >
    <template v-slot:activator="{ on, attrs }">
      <v-text-field
        v-model="dateRangeText"
        :label="label"
        prepend-inner-icon="event"
        background-color="#FFFFFF"
        outlined
        v-bind="attrs"
        clearable
        readonly
        :class="{ 'linha-data-xl-lg': $vuetify.breakpoint.xl || $vuetify.breakpoint.lg }"
        v-on="on"
      ></v-text-field>
    </template>
    <v-date-picker v-model="date" locale="pt-br" range no-title></v-date-picker>
  </v-menu>
</template>

<script>
export default {
  name: 'TemplateData',
  props: {
    label: {
      type: String,
      default: null
    }
  },
  data: () => ({
    menu: false,
    date: []
  }),
  computed: {
    dateRangeText: {
      set() {
        this.date = []
      },
      get() {
        return this.date.map(data => this.formatDate(data)).join(' Até ')
      }
    }
  },
  watch: {
    date(val) {
      this.$emit('atualizarData', val)
    }
  },
  methods: {
    formatDate(date) {
      if (!date) return null

      const [year, month, day] = date.split('-')
      return `${day}/${month}/${year}`
    },
    inicializarData() {
      this.date = []
    }
  }
}
</script>
<style scoped>
.linha-data-xl-lg {
  height: 58px;
}
</style>
