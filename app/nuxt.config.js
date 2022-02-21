const pkg = require("./package");

module.exports = {
  mode: "universal",

  /*
   ** Headers of the page
   */
  head: {
    title: "ghanjo-gag",
    meta: [
      { charset: "utf-8" },
      { name: "viewport", content: "width=device-width, initial-scale=1.0" },
      { hid: "description", name: "description", content: pkg.description },
    ],
    link: [
      { rel: "icon", type: "image/x-icon", href: "/favicon.png" },
      {
        rel: "stylesheet",
        href:
          "https://fonts.googleapis.com/css2?family=Roboto:ital,wght@0,300;0,500;0,700;1,300;1,500;1,700&display=swap",
      },
      {
        rel: "stylesheet",
        href:
          "https://fonts.googleapis.com/css2?family=Montserrat:ital,wght@0,100;0,200;0,300;0,400;0,500;0,600;0,700;0,800;0,900;1,100;1,200;1,300;1,400;1,500;1,600;1,700;1,800;1,900&display=swap",
      },
    ],
  },

  /*
   ** Customize the progress-bar color
   */
  loading: { color: "#fff" },

  /*x
   ** Global CSS
   */
  css: ["~/assets/reset.css", "~/assets/main.css"],

  /*
   ** Plugins to load before mounting the App
   */
  plugins: [
    { src: "~/plugins/request", ssr: false },
    { src: "~/plugins/userUtils", ssr: false },
    { src: "~/plugins/notifications-ssr", ssr: true },
    { src: "~/plugins/notifications-client", ssr: false },
    // '~/plugins/global.js',
    "~/plugins/filters.js",
  ],
  components: true,

  buildModules: ["@nuxtjs/style-resources"],

  styleResources: {
    scss: ["./assets/scss/mixins.scss", "./assets/scss/variables.scss"],
  },
  /*
   ** Nuxt.js modules
   */
  modules: [["@nuxtjs/moment"], ["@nuxtjs/toast", { iconPack: "fontawesome" }]],

  /*
   ** Build configuration
   */
  build: {
    /*
     ** You can extend webpack config here
     */
    extend(config) {
      config.module.rules.push({
        test: /\.(ogg|mp3|wav|mpe?g)$/i,
        loader: "file-loader",
        options: {
          name: "[path][name].[ext]",
        },
      });
      config.module.rules.push({
        test: /\.vue$/,
        loader: "vue-svg-inline-loader",
        options: {},
      });
    },
  },
  // router: {
  //   extendRoutes(routes, resolve) {
  //     routes.push({
  //       name: 'forgotpassword',
  //       path: '//forgotpassword',
  //       component: resolve(__dirname, 'pages/forgotpassword.vue')
  //     })
  //   }
  // },
  env: {
    api_uri:
      process.env.NODE_ENV !== "production"
        ? "http://localhost:3000"
        : "https://ghanjo.heddex.fr",
  },
  server: {
    port: 8080,
    host: "192.168.1.63",
    secure: true,
  },
};
