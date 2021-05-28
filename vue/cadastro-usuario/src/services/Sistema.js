import { HTTPClient } from '../utils/request'

export default {
  buscarParametro: async payload => {
    const { data } = await HTTPClient.get('/Sistema/BuscarParametro/' + payload)
    return data
  },
  buscarParametroFuncao: async payload => {
    const { data } = await HTTPClient.get('/Sistema/BuscarParametroFuncao')
    return data
  },
  atualizarParametro: async payload => {
    const { data } = await HTTPClient.post('/Sistema/AtualizarParametro', payload)
    return data
  },
  buscarParticularidade: async payload => {
    const { data } = await HTTPClient.get('/Sistema/BuscarParticularidade/' + payload)
    return data
  },
  permissao: async payload => {
    const { data } = await HTTPClient.get('/Sistema/BuscarPermissao')
    return data
  },
  descNavioViagem: async payload => {
    const { data } = await HTTPClient.get('/Sistema/AutoCompletarDescNavioViagem/' + payload)
    return data
  },
  pessoa: async payload => {
    const { data } = await HTTPClient.get('/Sistema/AutoCompletarPessoa/' + payload)
    return data
  },
  porto: async payload => {
    const { data } = await HTTPClient.get('/Sistema/AutoCompletarPorto/' + payload)
    return data
  }
}
