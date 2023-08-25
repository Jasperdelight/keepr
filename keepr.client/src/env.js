
export const dev = window.location.origin.includes('localhost')
export const baseURL = dev ? 'https://localhost:7045' : ''
export const useSockets = false
export const domain = 'dev-428swjdorr0tv1o6.us.auth0.com'
export const audience = 'classroom.com'
export const clientId = 'EGXaS8EdNIIVzHlWUhVgmOlwIhswu7yg'