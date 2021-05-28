import { HTTPClient } from '../utils/request'

export default {
  buscar: async payload => {
    const { data } = await HTTPClient.post('/Usuario/Buscar', payload)
    return data
  },
  gravar: async payload => {
    const { data } = await HTTPClient.post('/Usuario/Gravar', payload)
    return data
  },
  excluir: async payload => {
    const { data } = await HTTPClient.get('/Usuario/Excluir?id=' + payload)
    return data
  },
  status: async payload => {
    const { data } = await HTTPClient.get('/Usuario/Status/')
    return data
  }
}
