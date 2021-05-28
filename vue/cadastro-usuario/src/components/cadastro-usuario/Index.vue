<template>
  <v-container class="py-4 px-7" fluid>
    <Cabecalho />
    <TelaPrincipal />
  </v-container>
</template>

<script>
import Cabecalho from './Cabecalho'
import TelaPrincipal from './TelaPrincipal'

export default {
  components: { Cabecalho, TelaPrincipal }
}
</script>
