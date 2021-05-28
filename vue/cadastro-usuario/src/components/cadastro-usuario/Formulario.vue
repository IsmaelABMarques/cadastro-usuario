<template>
  <div>
    <div>
      <v-row no-gutters class="mx-4 my-4 wrap ">
        <v-col cols="12" xs="12">
          <span class="sub-titulo">Preencha os campos abaixo</span>
        </v-col>
      </v-row>
      <v-row no-gutters class="mx-4 wrap" :class="{ 'linha-xl-lg': isTelaXLOrTelaLG }">
        <v-col cols="12" xs="12" sm="6" md="3" class="pr-2">
        <v-text-field
              v-model="formularioGravacao.id"
              label="Código"
              background-color="#FFFFFF"
              autocomplete="off"
              outlined
              clearable
              disabled
            ></v-text-field>
        </v-col>
        <v-col cols="12" xs="12" sm="6" md="3" class="pr-2">
          <v-text-field
              v-model="formularioGravacao.nome"
              label="Nome"
              background-color="#FFFFFF"
              autocomplete="off"
              outlined
              clearable
            ></v-text-field>
        </v-col>
        <v-col cols="12" xs="12" sm="6" md="3" class="pr-2">
            <v-text-field
              v-model="formularioGravacao.senha"
              label="Senha"
              background-color="#FFFFFF"
              autocomplete="off"
              outlined
              clearable
            ></v-text-field>
          </v-col>
          <v-col cols="12" xs="12" sm="6" md="3" class="pr-2">
            <v-select
              v-model="formularioGravacao.status.id"
              :items="listaStatus"
              item-text="descricao"
              item-value="id"
              label="Status"
              outlined
            ></v-select>
        </v-col>
      </v-row>
      <v-row no-gutters class="mx-4 pb-2">
        <v-btn class="mr-2" color="primary" rounded @click="AbrirModalPesquisa">Pesquisar</v-btn>
        <v-btn class="mr-2" color="primary" rounded @click="Gravar">Gravar</v-btn>
        <v-btn class="mr-2" color="error"   rounded @click="Excluir">Excluir</v-btn>
        <v-btn class="mr-2" color="#1e90ff" rounded outlined @click="limpar">Limpar</v-btn>
      </v-row>
    </div>
    <v-dialog v-model="modalBusca">
      <v-card outlined>
        <v-row no-gutters class="mx-4 my-4 wrap ">
          <v-col cols="12" xs="12">
            <span class="sub-titulo">Pesquisar</span>
          </v-col>
        </v-row>
      <v-row class="mx-4 my-4 wrap " :class="{ 'linha-xl-lg': isTelaXLOrTelaLG }">
          <v-col cols="12" xs="12" sm="6" md="3" class="pr-2">
          <v-text-field
                v-model="formularioBusca.Id"
                label="Código"
                background-color="#FFFFFF"
                autocomplete="off"
                outlined
                clearable
                @keypress="ImpedirInsercaoLetras($event.key)"
                @paste="ImpedirInsercaoLetras($event.clipboardData.getData('Text'))"
              ></v-text-field>
          </v-col>
          <v-col cols="12" xs="12" sm="6" md="3" class="pr-2">
            <v-text-field
                v-model="formularioBusca.nome"
                label="Nome"
                background-color="#FFFFFF"
                autocomplete="off"
                outlined
                clearable
              ></v-text-field>
          </v-col>
          <v-col cols="12" xs="12" sm="6" md="3" class="pr-2">
              <v-text-field
                v-model="formularioBusca.senha"
                label="Senha"
                background-color="#FFFFFF"
                autocomplete="off"
                outlined
                clearable
              ></v-text-field>
            </v-col>
            <v-col cols="12" xs="12" sm="6" md="3" class="pr-2">
              <v-select
                v-model="formularioBusca.status.id"
                :items="listaStatus"
                item-text="descricao"
                item-value="id"
                label="Status"
                outlined
              ></v-select>
          </v-col>
        </v-row>
        <v-row no-gutters class="mx-4 pb-2">
        <v-btn class="mr-2" color="primary" rounded @click="Buscar">Pesquisar</v-btn>
        <v-btn class="mr-2" color="#1e90ff" rounded outlined @click="limparPesquisa">Limpar</v-btn>
      </v-row>
      
      <div v-if="listaUsuario.length > 0" >
        <v-row no-gutters class="mx-4 my-4 wrap ">
          <v-col cols="12" xs="12">
            <span class="sub-titulo">Usuários</span>
          </v-col>
        </v-row>
        <v-row class="d-block mx-4  mt-6">
          <v-card outlined>
            <v-data-table
              v-model="listaUsuarioSelecionados"
              elevation="0"
              :headers="headersBusca"
              :items="listaUsuario"
              item-key="id"
            >
            <template v-slot:[`item.id`]="{ item }">
              <v-btn class="mr-2" color="primary" rounded @click="Editar(item)">Editar</v-btn>
            </template>
            </v-data-table>
          </v-card>
        </v-row>
      </div>
      </v-card>
    </v-dialog>
  </div>
</template>

<script>
import { mapActions } from 'vuex'
import { usuarioService } from '@/services'
import TemplateData from './TemplateData.vue'
export default {
  name: 'FormularioPesquisa',
  components: { TemplateData },
  data: () => ({
    modalBusca: false,
    listaStatus: [],
    listaUsuario: [],
    listaUsuarioSelecionados: [],
    formularioBusca: {
      id: 0,
      nome: null,
      senha: null,
      status: { id: 0, descricao: null }
    },
    formularioGravacao: {
      id: 0,
      nome: null,
      senha: null,
      status: { id: 0, descricao: null }
    },
    headersBusca: [
      {
        text: 'Nome',
        value: 'nome',
        width: '25%'
      },
      {
        text: 'Senha',
        value: 'senha',
        width: '25%'
      },
      {
        text: 'Status',
        value: 'status.descricao',
        width: '25%'
      },
      {
        text: 'Opções',
        value: 'id',
        width: '25%'
      }
    ]
  }),
  computed: {
    isTelaXLOrTelaLG() {
      return this.$vuetify.breakpoint.xl || this.$vuetify.breakpoint.lg
    }
  },
  watch: {},
  mounted() {
    this.montarComboStatus()
  },
  methods: {
    AbrirModalPesquisa(){
      this.modalBusca = !this.modalBusca
    },
    Editar(item){
      this.formularioGravacao = item
      this.AbrirModalPesquisa()
      this.limparPesquisa()
    },
    async montarComboStatus() {
      try {
        this.listaStatus = await usuarioService.status()
      } catch (error) {
        this.setToast({
          time: 5000,
          color: 'error',
          message: 'Não foi possível buscar os Portos'
        })
      }
    },
    async Buscar() {
      this.listaUsuario = []
      this.setLoading('Buscando Usuários')
      try {
        if(this.formularioBusca.id == null || this.formularioBusca.id == "" || this.formularioBusca.isNaN)
          this.formularioBusca.id = 0
        if(this.formularioBusca.status.id == null || this.formularioBusca.status.id == "" || this.formularioBusca.status.isNaN)
          this.formularioBusca.status.id = 0
        this.listaUsuario = await usuarioService.buscar(this.formularioBusca)
        if (this.listaUsuario.length < 1)
          this.setToast({ time: 5000, color: 'error', message: 'Não foi encontrado nenhum Usuário' })
      } catch (error) {
        this.setToast({
          time: 5000,
          color: 'error',
          message: (error.response.data) || 'Não foi possível buscar os Usuários'
        })
      } finally {
        this.resetLoading()
      }
    },
    async Gravar() {
      var msg = 'Usuário cadastrado com sucesso'
      this.setLoading('Gravando Usuário')
      try {
        this.formularioGravacao.id = await usuarioService.gravar(this.formularioGravacao)
        this.setToast({
          time: 5000,
          color: 'success',
          message: msg
        })
      } catch (error) {
        console.log(error.response)
        this.setToast({
          time: 5000,
          color: 'error',
          message: (error.response.data) || 'Não foi possível gravar o Usuário'
        })
      } finally {
        this.resetLoading()
      }
    },
    async Excluir() {
      var msg = 'Usuário excluído com Sucesso'
      this.setLoading('Gravando Usuário')
      try {
        await usuarioService.excluir(this.formularioGravacao.id)
        this.listaUsuario.pop(this.formularioGravacao)
        this.limpar()
        this.setToast({
          time: 5000,
          color: 'success',
          message: msg
        })
      } catch (error) {
        this.setToast({
          time: 5000,
          color: 'error',
          message: (error.response.data) || 'Não foi possível excluir o Usuário'
        })
      } finally {
        this.resetLoading()
      }
    },
    ImpedirInsercaoLetras(obj) {
      for (var o in obj) {
        if (isNaN(parseFloat(obj[o]))) {
          event.preventDefault()
        }
      }
    },
    limparPesquisa() {
      Object.assign(this.$data.formularioBusca, this.$options.data().formularioBusca)
      this.listaUsuario = []
      this.listaUsuarioSelecionados = []
    },
    limpar() {
      Object.assign(this.$data.formularioGravacao, this.$options.data().formularioGravacao)
    },
    ...mapActions(['setToast', 'setLoading', 'resetLoading'])
  }
}
</script>
<style>
.sub-titulo {
  font-style: normal;
  font-weight: 500;
  font-size: 16px;
  line-height: 19px;
  letter-spacing: 0.15px;
  color: #000000;
}
.linha-xl-lg {
  height: 77px;
}
.titulo-erro {
  font-style: normal;
  font-weight: 500;
  font-size: 18px;
  line-height: 25px;
  letter-spacing: 0.75px;
  text-transform: capitalize;
  margin-bottom: 42px;
  color: #ce0101;
}
.span-download {
  color: #0000ff;
}
</style>
