import axios from 'axios'
import { DEFAULT_API_URLS } from './enum'
import { getStorage } from './localStorage'

const { stringify, parse } = JSON
export const parseNetworkError = error => parse(stringify(error))

const withBaseURLContext = () =>
  process.env.NODE_ENV ? DEFAULT_API_URLS[process.env.NODE_ENV.toUpperCase()] : DEFAULT_API_URLS.development

const HTTPClient = axios.create({
  baseURL: withBaseURLContext(),
  headers: {
    UsuarioId: getStorage('usuarioId') || 1,
    GrupoUsuarioId: getStorage('GrupoUsuarioId') || 1,
    EmpresaId: getStorage('EmpresaId') || 1
  }
})

export { HTTPClient }
