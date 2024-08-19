// https://nuxt.com/docs/api/configuration/nuxt-config
export default defineNuxtConfig({
    compatibilityDate: '2024-04-03',
    devtools: {enabled: true},
    modules: [
        "@nuxtjs/tailwindcss",
        "shadcn-nuxt",
        "@nuxtjs/color-mode"
    ],
    colorMode: {
        classSuffix: ''
    },
    runtimeConfig: {
        // Make environment variables accessible here
        public: {
            API_BASE_URL: process.env.API_BASE_URL,
            // Add more environment variables as needed
        },
    },
})