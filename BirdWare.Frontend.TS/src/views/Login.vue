<template>
    <div class="center-container">
        <div class="text-black dark:text-birdware-bright">
            <form class="border border-gray-400 p-4 rounded" @submit.prevent="login">
                <div class="mb-3">
                    <input type="text" class="p-2 w-80 border border-gray-200 rounded" placeholder="Brugernavn"
                        name="uname" ref="username" v-focus v-model="state.username" />
                </div>
                <div class="mb-3">
                    <input class="p-2 w-80 border border-gray-200 rounded" type="password" placeholder="Kodeord"
                        name="pwd" ref="pwd" v-model="state.password" />
                </div>
                <div class="mb-2">
                    <button type="submit" class="cursor-pointer p-2 w-80 border border-gray-400 rounded text-gray-800 dark:text-white"
                        :disabled="!canLogin">Log ind</button>
                </div>
            </form>
        </div>
    </div>

</template>

<script setup lang="ts">
import api from '@/api';
import { useAuthenticateStore } from '@/stores/authenticate.ts';
import { reactive, computed } from 'vue';
import { useRouter } from 'vue-router'
import { useRoute } from 'vue-router'

const authenticate = useAuthenticateStore();
const router = useRouter();
const route = useRoute();

const state = reactive({
    username: '',
    password: ''
});

const canLogin = computed(() => {
    return state.username.length > 0 && state.password.length > 0;
});

function login() {
    const auth = { username: state.username, password: state.password };
    api.post('auth/login', auth)
        .then(function (response) {
            authenticate.setJwtToken(response.data.accessToken);
        })
        .finally(() => {
            router.replace({ path: (route.query.redirect as string) || 'home' });
        });
}
</script>

<style scoped>
form {
    padding: 15px;
    margin: 5px;
}

.btn {
    width: 100%;
}

.center-container {
    width: 350px;
    height: 40%;
    overflow: auto;
    margin: auto;
    position: absolute;
    top: 0;
    bottom: 0;
    left: 0;
    right: 0;
}
</style>