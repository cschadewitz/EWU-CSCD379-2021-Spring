import '../styles/site.css';

import { library, dom } from "@fortawesome/fontawesome-svg-core";
import { fas } from '@fortawesome/free-solid-svg-icons';
import { far } from '@fortawesome/free-regular-svg-icons';
import { fab } from '@fortawesome/free-brands-svg-icons';

library.add(fas, far, fab);
dom.watch();

export function setupNavList() {
    return {
        toggleMenu() {
            var navList = document.getElementById('navList');
            if (navList) {
                if (navList.classList.contains('hidden')) {
                    navList.classList.remove('hidden');
                } else {
                    navList.classList.add('hidden');
                }
            }
        }
    }
}