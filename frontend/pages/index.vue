<script lang="ts" setup>
import {Button} from '@/components/ui/button'
import {Card, CardContent, CardDescription, CardFooter, CardHeader, CardTitle} from '@/components/ui/card'
import {Input} from '@/components/ui/input'
import {Label} from '@/components/ui/label'
import {useToast} from "~/components/ui/toast";
import {ref} from "vue";
import {Loader2} from "lucide-vue-next";

const username = ref('')
const password = ref('')
const router = useRouter()
const isLoading = ref(false);
const {toast} = useToast()
const config = useRuntimeConfig();

const handleSubmit = async () => {
  try {
    isLoading.value = true
    const response = await fetch(`${config.public.API_BASE_URL}/api/user/login`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; x-api-version=1.0'
      },
      body: JSON.stringify({
        username: username.value,
        password: password.value
      })
    });

    if (response.ok) {
      // Aqui você pode adicionar lógica para o que acontece após o login bem-sucedido.
      const result = await response.json();
      await router.push("/dashboard")
      console.log('Login bem-sucedido:', result);
    } else {
      // Aqui você pode lidar com erros.
      let data = await response.json()
      toast({
        title: 'Erro ao fazer login',
        description: data.message,
        duration: 3000,
        variant: 'destructive'
      });
    }
  } catch (error) {
    console.error('Erro de rede:', error);
  }
  finally {
    isLoading.value = false
  }
}

</script>

<template>
  <div class="min-h-screen flex justify-center items-center">
    <Card class="w-full max-w-sm">
      <CardHeader>
        <CardTitle class="text-2xl">
          Login
        </CardTitle>
        <CardDescription>
          Entre com seus dados abaixo
        </CardDescription>
      </CardHeader>
      <CardContent class="grid gap-4">
        <div class="grid gap-2">
          <Label for="username">Nome de usuário</Label>
          <Input id="username" v-model="username" autocomplete="off" placeholder="demo" required type="text"/>
        </div>
        <div class="grid gap-2">
          <Label for="password">Senha</Label>
          <Input id="password" v-model="password" autocomplete="off" placeholder="demo" required type="password"/>
        </div>
      </CardContent>
      <CardFooter>
        <Button class="w-full" @click="handleSubmit" :disabled="isLoading">
          <Loader2 class="w-4 h-4 animate-spin" v-if="isLoading"/>
          Entrar
        </Button>
      </CardFooter>
    </Card>
  </div>
</template>