<template>
    <div>
        <h1 id="customersTitle">
            Manage Customers
        </h1>
        <hr/>

        <div class="customer-actions">
            <solar-button @button:click="showNewCustomerModal">
                Add Customer
            </solar-button>
        </div>
        <table class="table" id="customer">
            <tr>
                <th>Customer</th>
                <th>Address</th>
                <th>County</th>
                <th>PostCode</th>
                <th>Since</th>
                <th>Delete</th>
            </tr>
            <tr :key="customer.id" v-for="customer in customers">
                <td>
                    {{customer.firstName}} {{customer.lastName}}
                </td>
                <td>
                    {{customer.primaryAddress.addressLine1}}
                </td>
                <td>
                    {{customer.primaryAddress.county}}
                </td>
                <td>
                    {{customer.primaryAddress.postalCode}}
                </td>
                <td>
                    {{customer.createdOn | toHumanReadableDate}}
                </td>
                <td>
                    <div @click="deleteCustomer(customer.id)" class="lni lni-cross-circle customer-delete">
                    </div>
                </td>
            </tr>
        </table>
        <new-customer-modal
                @close="closeModal"
                @save:customer="saveNewCustomer"
                v-if="isCustomerModalVisible"
        />
    </div>
</template>

<script lang="ts">
    import {Component, Vue} from 'vue-property-decorator'
    import SolarButton from '@/components/SolarButton.vue'
    import {ICustomer} from "@/types/Customer";
    import CustomerService from "@/services/customer-service";
    import NewCustomerModal from "@/components/modals/NewCustomerModal.vue";


    const customerService = new CustomerService();

    @Component({
        name: 'Customers',
        components: {SolarButton, NewCustomerModal}
    })
    export default class extends Vue {
        customers: ICustomer[] = [];
        isCustomerModalVisible: boolean = false;

        showNewCustomerModal() {
            this.isCustomerModalVisible = true;
        }

        closeModal() {
            this.isCustomerModalVisible = false;
        }

        async saveNewCustomer(newCustomer: ICustomer) {
            await customerService.addCustomer(newCustomer);
            this.isCustomerModalVisible = false;
            await this.initialize();
        }

        async deleteCustomer(customerId: number) {
            await customerService.deleteCustomer(customerId);
            await this.initialize()
            ;
        }

        async initialize() {
            this.customers = await customerService.getCustomers();
        }

        async created() {
            await this.initialize();
        }
    }
</script>

<style lang="scss" scoped>
    @import "@/scss/global.scss";

    .customer-actions {
        display: flex;
        margin-bottom: 0.8rem;

        div {
            margin-right: 0.8rem;
        }
    }

    .customer-delete {
        cursor: pointer;
        font-weight: bold;
        font-size: 1.2rem;
        color: $solar-red;
    }
</style>