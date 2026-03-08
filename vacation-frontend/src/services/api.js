const API_BASE_URL = import.meta.env.VITE_API_BASE_URL || 'http://localhost:5000'

function buildUrl(path, params) {
  const url = new URL(`${API_BASE_URL}${path}`)
  if (params) {
    Object.entries(params).forEach(([key, value]) => {
      if (value === undefined || value === null || value === '') return
      url.searchParams.set(key, String(value))
    })
  }
  return url.toString()
}

async function request(path, { params, method = 'GET', body } = {}) {
  const url = params ? buildUrl(path, params) : `${API_BASE_URL}${path}`
  const options = {
    method,
    headers: {
      'Content-Type': 'application/json'
    }
  }
  if (body !== undefined) {
    options.body = JSON.stringify(body)
  }

  const response = await fetch(url, options)
  if (response.status === 204) return null

  const text = await response.text()
  if (!response.ok) {
    const message = text || `Request failed with status ${response.status}`
    throw new Error(message)
  }

  return text ? JSON.parse(text) : null
}

export function getEmployees(filters) {
  return request('/api/Employee', { params: filters })
}

export function getEmployeeById(id) {
  return request(`/api/Employee/${id}`)
}

export function createEmployee(payload) {
  return request('/api/Employee', { method: 'POST', body: payload })
}

export function updateEmployee(id, payload) {
  return request(`/api/Employee?id=${id}`, { method: 'PUT', body: payload })
}

export function deleteEmployee(id) {
  return request(`/api/Employee/${id}`, { method: 'DELETE' })
}

export function getDepartments() {
  return request('/api/Setting/departments')
}

export function getRoles() {
  return request('/api/Setting/Roles')
}
