import './styles.css'
import { createApp } from 'vue'
import App from './App.vue'
import { router } from './router'
import { library } from '@fortawesome/fontawesome-svg-core'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'

import { faUser, faGear, faHouse, faPlus, faPenToSquare, faTrash, faCircleXmark, faBan, faFloppyDisk} from '@fortawesome/free-solid-svg-icons'

library.add(faUser, faGear, faHouse, faPlus, faPenToSquare, faTrash, faCircleXmark, faBan, faFloppyDisk)

const app = createApp(App)
app.component('fa-icon', FontAwesomeIcon)
app.use(router)
app .mount('#app')
