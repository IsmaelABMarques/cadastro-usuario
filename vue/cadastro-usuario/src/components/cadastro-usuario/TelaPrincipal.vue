<template>
  <main>
    <v-card  outlined>
      <v-tabs v-model="tab">
        <v-tab href="#tab-1">Cadastro</v-tab>
      </v-tabs>
      <v-divider class="mb-0 pb-2"></v-divider>
      <v-tabs-items v-model="tab" class="mx-3  pb-2">
        <v-tab-item value="tab-1">
          <FormularioPesquisa />
        </v-tab-item>
      </v-tabs-items>
    </v-card>
  </main>
</template>

<script>
import FormularioPesquisa from './Formulario'
import Configuracoes from './Configuracoes'
import { sistemaService } from '@/services'

export default {
  name: 'TelaPrincipal',
  components: { FormularioPesquisa, Configuracoes },
  data: () => ({
    tab: null
  }),
  mounted() {
  },
  methods: {
  }
}
</script>
