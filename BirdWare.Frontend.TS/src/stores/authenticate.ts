import { ref, computed } from 'vue'
import { defineStore } from 'pinia'

export const useAuthenticateStore = defineStore('authenticate', () => {

    const jwtToken = ref(null as string | null);

    function setJwtToken(token: string | null) {
        jwtToken.value = token;
    }

    const isLoggedIn = computed(() => {
        return jwtToken.value != null;
    });

    return { jwtToken, setJwtToken, isLoggedIn };
});