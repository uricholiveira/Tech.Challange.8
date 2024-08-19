<template>
  <div>
    <NuxtLoadingIndicator/>
    <NuxtLayout>
      <NuxtPage/>
      <Toaster/>
    </NuxtLayout>
  </div>
</template>
<script lang="ts" setup>
import {Toaster} from "~/components/ui/toast";

const colorMode = useColorMode()
colorMode.preference = 'dark'
</script>