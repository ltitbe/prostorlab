<template  :key="componentKey">
    <div class="users">
        <div>
            <form @submit.prevent="addUser">
                <table>
                    <thead>
                        <tr>
                            <th>Новый пользователь</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>                            
                            <th><input v-model.trim="firstName" type="text" id="firstName" placeholder="Имя" required /></th>
                            <th><input v-model.trim="lastName" type="text" id="lastName" placeholder="Фамилия" required /></th>
                            <th><button type="submit">Добавить</button></th>
                        </tr>
                    </tbody>
                </table>
            </form>            
        </div>

        <h4 v-if="users.length == 0">Пользователи не найдены</h4>
        <div>
            <table>
                <thead>
                    <tr>
                        <th>Имя</th>
                        <th>Фамилия</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="user in users" :key="user.id">
                        <td>{{ user.firstName }}</td>
                        <td>{{ user.lastName }}</td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</template>

<script lang="js">
    import { defineComponent } from 'vue';
    import axios from 'axios';
    export default defineComponent({
        name: 'UserTable',                
        data() {            
            return { users: [], componentKey : 0 }
        },
        methods: {
            async fetchData() {
                await axios
                    .get('https://localhost:7112/api/users')
                    .then((usersResponse) => {
                        this.users = usersResponse.data
                        console.log(this.users)
                        this.componentKey += 1;
                    })
                    .catch(error => {
                        console.log(error)
                    });                
            },
            async addUser() {
                console.log(this.firstName + this.lastName)
                await axios
                    .post('https://localhost:7112/api/users', { firstName: this.firstName, lastName: this.lastName })
                    .then(() => {
                        this.firstName = '';
                        this.lastName = ''
                    })
                    .catch(error => {
                        console.log(error)
                    })
                await this.fetchData();                
            }
        },
        mounted() {
            this.fetchData()
        }
    });
</script>

<style>
</style>