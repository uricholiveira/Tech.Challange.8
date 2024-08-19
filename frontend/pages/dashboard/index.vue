<script lang="ts" setup>
import {
  CircleUser,
  File,
  ListFilter,
  MoreHorizontal,
  Package2,
  PanelLeft,
  PlusCircle,
  Search,
  Settings,
  Users2,
  Loader2
} from 'lucide-vue-next'

import {
  Select,
  SelectContent,
  SelectGroup,
  SelectItem,
  SelectLabel,
  SelectTrigger,
  SelectValue,
} from '@/components/ui/select'

import {
  Pagination,
  PaginationEllipsis,
  PaginationFirst,
  PaginationLast,
  PaginationList,
  PaginationListItem,
  PaginationNext,
  PaginationPrev,
} from '@/components/ui/pagination'

import {Dialog, DialogContent, DialogFooter, DialogHeader, DialogTitle, DialogTrigger,} from '@/components/ui/dialog'

import {Badge} from '@/components/ui/badge'
import {Button} from '@/components/ui/button'
import {Card, CardContent, CardDescription, CardFooter, CardHeader, CardTitle} from '@/components/ui/card'
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuSeparator,
  DropdownMenuTrigger
} from '@/components/ui/dropdown-menu'
import {Input} from '@/components/ui/input'
import {Breadcrumb, BreadcrumbItem, BreadcrumbList, BreadcrumbPage,} from '@/components/ui/breadcrumb'
import {Sheet, SheetContent, SheetTrigger} from '@/components/ui/sheet'
import {Table, TableBody, TableCell, TableHead, TableHeader, TableRow,} from '@/components/ui/table'
import {Tabs, TabsContent,} from '@/components/ui/tabs'
import {Tooltip, TooltipContent, TooltipProvider, TooltipTrigger,} from '@/components/ui/tooltip'
import {NumberField, NumberFieldContent, NumberFieldDecrement, NumberFieldIncrement, NumberFieldInput} from "@/components/ui/number-field";
import {useToast} from "@/components/ui/toast";
import { useRouter } from "vue-router";
import { ref, reactive, onMounted } from 'vue'
import {getEnv} from "nitropack/runtime/utils.env";

const page = ref(1);
const pageSize = ref(10);
const pageSizes = ref([10, 20, 30, 40, 50, 60, 70, 80, 100]);
const isLoading = ref(false);
const count = ref(1)

const users = reactive([] as any[]);

const pagination = reactive({
  currentPage: 1,
  totalPages: 0,
  totalCount: 0,
});

const { toast } = useToast()

const handleGet = async () => {
  isLoading.value = true;
  try {
    console.log('Página:', page.value, 'Itens por página:', pageSize.value);

    const response = await fetch(`${config.public.API_BASE_URL}/api/users?page=${page.value}&pageSize=${pageSize.value}`, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
        'x-api-version': '1.0'
      }
    });

    if (response.ok) {
      const data = await response.json();
      console.log('Dados recebidos:', data); // Log dos dados recebidos

      users.splice(0, users.length, ...data.data);
      pagination.currentPage = data.pagination.currentPage;
      pagination.totalPages = data.pagination.totalPages;
      pagination.totalCount = data.pagination.totalCount;
    } else {
      console.error('Erro ao buscar dados:', response.statusText);
    }
  } catch (error) {
    console.error('Erro de rede:', error);
  } finally {
    isLoading.value = false;
  }
};

const config = useRuntimeConfig();

onMounted(async () => {
  await handleGet();
});

const handlePageChange = async (newPage: number) => {
  page.value = newPage;
  await handleGet();
};

const handlePageSizeChange = async (newPageSize: number) => {
  pageSize.value = newPageSize;
  page.value = 1; // Reiniciando a página para 1 ao mudar o tamanho da página
  await handleGet();
};

const handleSubmit = async () => {
  try {
    const response = await fetch(`${config.public.API_BASE_URL}/api/user`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        'x-api-version': '1.0'
      },
      body: JSON.stringify({ count: count.value }) // Ajustado para usar um valor constante
    });

    if (response.ok) {
      await handleGet();
      toast({ title: "Usuário gerado com sucesso", variant: "success" });
    } else {
      console.error('Erro ao criar usuário:', response.statusText);
    }
  } catch (error) {
    console.error('Erro de rede:', error);
  }
};

const handleDelete = async (id: string) => {
  try {
    const response = await fetch(`${config.public.API_BASE_URL}/api/user/${id}`, {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json',
        'x-api-version': '1.0'
      }
    });

    if (response.ok) {
      await handleGet();
      toast({ title: "Usuário deletado com sucesso", variant: "success" });
    } else {
      console.error('Erro ao deletar usuário:', response.statusText);
      let responseData = await response.json()
      toast({ title: "Erro ao deletar usuário", variant: "destructive", description: responseData.message });
    }
  } catch (error) {
    console.error('Erro de rede:', error);
  }
};
</script>

<template>
  <div class="flex min-h-screen w-full flex-col bg-muted/40">
    <aside class="fixed inset-y-0 left-0 z-10 hidden w-14 flex-col border-r bg-background sm:flex">
      <nav class="flex flex-col items-center gap-4 px-2 py-4">
        <a
            class="group flex h-9 w-9 shrink-0 items-center justify-center gap-2 rounded-full bg-primary text-lg font-semibold text-primary-foreground md:h-8 md:w-8 md:text-base"
            href="#"
        >
          <Package2 class="h-4 w-4 transition-all group-hover:scale-110"/>
          <span class="sr-only">Acme Inc</span>
        </a>
        <TooltipProvider>
          <Tooltip>
            <TooltipTrigger as-child>
              <a
                  class="flex h-9 w-9 items-center justify-center rounded-lg bg-accent text-accent-foreground transition-colors hover:text-foreground md:h-8 md:w-8"
                  href="#"
              >
                <Users2 class="h-5 w-5"/>
                <span class="sr-only">Usuários</span>
              </a>
            </TooltipTrigger>
            <TooltipContent side="right">
              Usuários
            </TooltipContent>
          </Tooltip>
        </TooltipProvider>
      </nav>
      <nav class="mt-auto flex flex-col items-center gap-4 px-2 py-4">
        <TooltipProvider>
          <Tooltip>
            <TooltipTrigger as-child>
              <a
                  class="flex h-9 w-9 items-center justify-center rounded-lg text-muted-foreground transition-colors hover:text-foreground md:h-8 md:w-8"
                  href="#"
              >
                <Settings class="h-5 w-5"/>
                <span class="sr-only">Settings</span>
              </a>
            </TooltipTrigger>
            <TooltipContent side="right">
              Settings
            </TooltipContent>
          </Tooltip>
        </TooltipProvider>
      </nav>
    </aside>
    <div class="flex flex-col sm:gap-4 sm:py-4 sm:pl-14">
      <header
          class="sticky top-0 z-30 flex h-14 items-center gap-4 border-b bg-background px-4 sm:static sm:h-auto sm:border-0 sm:bg-transparent sm:px-6">
        <Sheet>
          <SheetTrigger as-child>
            <Button class="sm:hidden" size="icon" variant="outline">
              <PanelLeft class="h-5 w-5"/>
              <span class="sr-only">Toggle Menu</span>
            </Button>
          </SheetTrigger>
          <SheetContent class="sm:max-w-xs" side="left">
            <nav class="grid gap-6 text-lg font-medium">
              <a
                  class="group flex h-10 w-10 shrink-0 items-center justify-center gap-2 rounded-full bg-primary text-lg font-semibold text-primary-foreground md:text-base"
                  href="#"
              >
                <Package2 class="h-5 w-5 transition-all group-hover:scale-110"/>
                <span class="sr-only">Acme Inc</span>
              </a>
              <a
                  class="flex items-center gap-4 px-2.5 text-muted-foreground hover:text-foreground"
                  href="#"
              >
                <Users2 class="h-5 w-5"/>
                Usuários
              </a>
            </nav>
          </SheetContent>
        </Sheet>
        <Breadcrumb class="hidden md:flex">
          <BreadcrumbList>
            <BreadcrumbItem>
              <BreadcrumbPage>
                <a href="#">Usuários</a>
              </BreadcrumbPage>
            </BreadcrumbItem>
          </BreadcrumbList>
        </Breadcrumb>
        <div class="relative ml-auto flex-1 md:grow-0">
          <Search class="absolute left-2.5 top-2.5 h-4 w-4 text-muted-foreground"/>
          <Input
              class="w-full rounded-lg bg-background pl-8 md:w-[200px] lg:w-[320px]"
              placeholder="Pesquisar..."
              type="search"
          />
        </div>
        <DropdownMenu>
          <DropdownMenuTrigger as-child>
            <Button class="rounded-full" size="icon" variant="secondary">
              <CircleUser class="h-5 w-5"/>
              <span class="sr-only">Toggle user menu</span>
            </Button>
          </DropdownMenuTrigger>
          <DropdownMenuContent align="end">
            <DropdownMenuLabel>Minha conta</DropdownMenuLabel>
            <DropdownMenuSeparator/>
            <DropdownMenuItem>Configurações</DropdownMenuItem>
            <DropdownMenuItem>Ajuda</DropdownMenuItem>
            <DropdownMenuSeparator/>
            <DropdownMenuItem @click="$router.push('/')">Sair</DropdownMenuItem>
          </DropdownMenuContent>
        </DropdownMenu>
      </header>
      <main class="grid flex-1 items-start gap-4 p-4 sm:px-6 sm:py-0 md:gap-8">
        <Tabs default-value="all">
          <div class="flex items-center">
            <div class="ml-auto flex items-center gap-2">
              <DropdownMenu>
                <DropdownMenuTrigger as-child>
                  <Button class="h-7 gap-1" disabled size="sm" variant="outline">
                    <ListFilter class="h-3.5 w-3.5"/>
                    <span class="sr-only sm:not-sr-only sm:whitespace-nowrap">
                      Filtros
                    </span>
                  </Button>
                </DropdownMenuTrigger>
                <DropdownMenuContent align="end">
                  <DropdownMenuLabel>Filtrar por</DropdownMenuLabel>
                  <DropdownMenuSeparator/>
                  <DropdownMenuItem checked>
                    Ativos
                  </DropdownMenuItem>
                </DropdownMenuContent>
              </DropdownMenu>
              <Button class="h-7 gap-1" disabled size="sm" variant="outline">
                <File class="h-3.5 w-3.5"/>
                <span class="sr-only sm:not-sr-only sm:whitespace-nowrap">
                  Exportar
                </span>
              </Button>
              <Button class="h-7 gap-1" size="sm" @click="handleGet" :disabled="isLoading">
                <Loader2 class="w-4 h-4 animate-spin" v-if="isLoading"/>
                <span class="sr-only sm:not-sr-only sm:whitespace-nowrap">
                      Atualizar
                    </span>
              </Button>
              <Dialog>
                <DialogTrigger as-child>
                  <Button class="h-7 gap-1" size="sm">
                    <PlusCircle class="h-3.5 w-3.5"/>
                    <span class="sr-only sm:not-sr-only sm:whitespace-nowrap">
                      Gerar usuário
                    </span>
                  </Button>
                </DialogTrigger>
                <DialogContent class="sm:max-w-[425px]">
                  <DialogHeader>
                    <DialogTitle>Gerar usuários</DialogTitle>
                  </DialogHeader>
                  <div class="grid gap-4 py-4">
                    <div class="">
                      <NumberField id="count" v-model="count" :default-value="1" :min="1">
                        <Label>Quantidade de Registros</Label>
                        <NumberFieldContent>
                          <NumberFieldDecrement/>
                          <NumberFieldInput/>
                          <NumberFieldIncrement/>
                        </NumberFieldContent>
                      </NumberField>
                    </div>
                  </div>
                  <DialogFooter>
                    <DialogClose as-child>
                      <Button type="submit" @click="handleSubmit">
                        Gerar
                      </Button>
                    </DialogClose>
                  </DialogFooter>
                </DialogContent>
              </Dialog>
            </div>
          </div>
          <TabsContent value="all">
            <Card>
              <CardHeader>
                <CardTitle>Usuários</CardTitle>
                <CardDescription>Visualize, e crie novos usuários</CardDescription>
              </CardHeader>
              <CardContent>
                <Table>
                  <TableHeader>
                    <TableRow>
                      <TableHead class="hidden w-[100px] sm:table-cell">
                        <span class="sr-only">img</span>
                      </TableHead>
                      <TableHead>Nome</TableHead>
                      <TableHead>Usuário</TableHead>
                      <TableHead class="hidden md:table-cell">Email</TableHead>
                      <TableHead class="hidden md:table-cell">Telefone</TableHead>
                      <TableHead class="hidden md:table-cell">Criado em</TableHead>
                      <TableHead><span class="sr-only">Actions</span></TableHead>
                    </TableRow>
                  </TableHeader>
                  <TableBody>
                    <TableRow v-for="user in users" :key="user.id">
                      <TableCell class="hidden sm:table-cell">
                        <img :src="user.picture ? user.picture.thumbnail : '~/assets/images/avatar_fallback.png'" alt="user avatar"/>
                      </TableCell>
                      <TableCell class="font-medium">
                        {{ user.name.first + ' ' + user.name.last }}
                      </TableCell>
                      <TableCell>
                        <Badge variant="outline">{{ user.login.username }}</Badge>
                      </TableCell>
                      <TableCell class="hidden md:table-cell">{{ user.email }}</TableCell>
                      <TableCell class="hidden md:table-cell">{{ user.phone }}</TableCell>
                      <TableCell class="hidden md:table-cell">{{ new Date(user.createdAt).toLocaleString() }}</TableCell>
                      <TableCell>
                        <DropdownMenu>
                          <DropdownMenuTrigger as-child>
                            <Button aria-haspopup="true" size="icon" variant="ghost">
                              <MoreHorizontal class="h-4 w-4"/>
                              <span class="sr-only">Toggle menu</span>
                            </Button>
                          </DropdownMenuTrigger>
                          <DropdownMenuContent align="end">
                            <DropdownMenuLabel>Ações</DropdownMenuLabel>
                            <DropdownMenuItem @click="handleDelete(user.id)">Deletar</DropdownMenuItem>
                          </DropdownMenuContent>
                        </DropdownMenu>
                      </TableCell>
                    </TableRow>
                  </TableBody>
                </Table>
              </CardContent>
              <CardFooter>
                <div class="w-full flex justify-between">
                  <Pagination :items-per-page="pageSize" :page="pagination.currentPage"
                              :sibling-count="1" :total="pagination.totalCount" show-edges
                              @update:page="handlePageChange">
                    <PaginationList v-slot="{ items }" class="flex items-center gap-1">
                      <PaginationFirst/>
                      <PaginationPrev/>
                      <template v-for="(item, index) in items">
                        <PaginationListItem v-if="item.type === 'page'" :key="index" :value="item.value" as-child>
                          <Button :variant="item.value === pagination.currentPage ? 'default' : 'outline'" class="w-10 h-10 p-0">
                            {{ item.value }}
                          </Button>
                        </PaginationListItem>
                        <PaginationEllipsis v-else :key="item.type" :index="index"/>
                      </template>
                      <PaginationNext/>
                      <PaginationLast/>
                    </PaginationList>
                  </Pagination>
                  <Select @update:value="handlePageSizeChange" disabled>
                    <SelectTrigger class="w-[180px]">
                      <SelectValue placeholder="Tamanho da Página"/>
                    </SelectTrigger>
                    <SelectContent>
                      <SelectGroup>
                        <SelectLabel>Tamanhos de Página</SelectLabel>
                        <SelectItem v-for="size in pageSizes" :key="size" :value="size">
                          {{ size }}
                        </SelectItem>
                      </SelectGroup>
                    </SelectContent>
                  </Select>
                </div>
              </CardFooter>
            </Card>
          </TabsContent>
        </Tabs>
      </main>
    </div>
  </div>
</template>