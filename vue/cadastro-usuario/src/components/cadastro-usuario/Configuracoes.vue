<template>
  <div class="mt-2">
    <v-row no-gutters class="mx-4 my-4 wrap ">
      <v-col cols="12" xs="12">
        <span class="sub-titulo">Parâmetros da função</span>
      </v-col>
    </v-row>
    <v-row
      v-for="item in parametros"
      :key="item.id"
      no-gutters
      class="mx-4 wrap"
      :class="{ 'linha-xl-lg': isTelaXLOrTelaLG }"
    >
      <v-text-field
        v-model="item.parametro"
        :label="item.nome"
        :hint="item.descricao"
        background-color="#FFFFFF"
        autocomplete="off"
        outlined
        clearable
      ></v-text-field>
    </v-row>
    <v-row no-gutters class="mx-4 pb-2">
      <v-btn class="mr-2" color="primary" rounded @click="gravar">Gravar</v-btn>
    </v-row>
  </div>
</template>

<script>
import { sistemaService } from '@/services'
import { mapActions } from 'vuex'
export default {
  name: 'Configuracoes',
  data: () => ({
    tab: null,
    parametros: []
  }),
  computed: {
    isTelaXLOrTelaLG() {
      return this.$vuetify.breakpoint.xl || this.$vuetify.breakpoint.lg
    }
  },
  mounted() {
    this.buscarParametros()
  },
  methods: {
    testar() {
      console.log(this.parametros)
    },
    async buscarParametros() {
      var parametros = await sistemaService.buscarParametroFuncao()
      this.parametros = parametros
    },
    async gravar() {
      this.setLoading('Gravando')
      try {
        for (var param in this.parametros) {
          console.log(param)
          await sistemaService.atualizarParametro(this.parametros[param])
        }
        this.setToast({ time: 5000, color: 'success', message: 'Gravação Concluída' })
      } catch (error) {
        var errorMessage = 'Não foi possível atualizar os parâmetros'
        this.setToast({
          time: 5000,
          color: 'error',
          message: (error.response && error.response.data.Message) || errorMessage
        })
      } finally {
        this.resetLoading()
      }
    },
    ...mapActions(['setToast', 'setLoading', 'resetLoading'])
  }
}
</script>
