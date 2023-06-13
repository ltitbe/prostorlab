<template :key="componentKey">
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
                            <th>
                                <input v-model.trim="firstNameAdd" type="text" placeholder="Имя" required />
                                <input v-model.trim="lastNameAdd" type="text" placeholder="Фамилия" required />
                                <button type="submit">Добавить</button>
                            </th>
                        </tr>
                    </tbody>
                </table>
            </form>
        </div>
        <h2 v-if="users.length == 0">Пользователи не найдены</h2>
        <div v-else>
            <form>
                <table>
                    <thead>
                        <tr>
                            <th>Имя</th>
                            <th>Фамилия</th>
                        </tr>
                    </thead>
                    <tbody>
                        <template v-for="user in users" :key="user.id">
                            <tr>
                                <td>{{ user.firstName }}</td>
                                <td>{{ user.lastName }}</td>
                                <td>
                                    <button type="button" @click="edit=true; userIdEdit = user.id; firstNameEdit=user.firstName; lastNameEdit=user.lastName;">Редактировать</button>
                                    <button type="submit" @click="deleteUser(user.id, user.firstName, user.lastName)">Удалить</button>
                                </td>
                            </tr>
                        </template>
                    </tbody>
                </table>
            </form>
        </div>
    </div>

    <div v-if="edit" class="modal">
        <div class="modal-content">
            <form @submit.prevent="editUser">
                <p>Редактировать пользователя</p>
                <label>Имя:</label><input v-model.trim="firstNameEdit" type="text" placeholder="Имя" required />
                <label>Фамилия:</label><input v-model.trim="lastNameEdit" type="text" placeholder="Фамилия" required />
                <p>
                    <button type="submit">Сохранить</button>
                    <button type="button" @click="edit=false">Отменить</button>
                </p>                
            </form>
        </div>        
    </div>
</template>

<script lang="js">
    import { defineComponent } from 'vue';
    import axios from 'axios';
    export default defineComponent({
        name: 'UserTable',                
        data() {            
            return { users: [], componentKey: 0, edit: false}
        },
        methods: {
            async getUsers() {
                await axios
                    .get('https://localhost:7112/api/users')
                    .then((usersResponse) => {
                        this.users = usersResponse.data
                        this.componentKey += 1;
                    })
                    .catch(error => {
                        console.log(error)
                    });                
            },
            async addUser() {
                await axios
                    .post('https://localhost:7112/api/users', { firstName: this.firstNameAdd, lastName: this.lastNameAdd })
                    .then(async () => {
                        this.firstNameAdd = '';
                        this.lastNameAdd = ''
                        await this.getUsers();
                    })
                    .catch(error => {
                        console.log(error)
                    })                                
            },
            async deleteUser(id, firstName, lastName) {
                if (confirm(`Вы уверены, что хотите удалить пользователя ${firstName} ${lastName}?`)) {
                    await axios
                        .delete('https://localhost:7112/api/users/' + id)
                        .then(async () => { await this.getUsers() })
                        .catch(error => {
                            console.log(error);
                        })
                }
            },
            async editUser() {
                await axios
                    .put('https://localhost:7112/api/users/' + this.userIdEdit, { firstName: this.firstNameEdit, lastName: this.lastNameEdit })
                    .then(async () => {
                        this.edit = false,
                        await this.getUsers()
                    })
                    .catch(error => {
                        console.log(error);
                    })
            }            
        },
        created() {
            this.getUsers()
        }
    });
</script>