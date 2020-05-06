<template>
    <div>
        <h1 id="invoiceTitle">
            Create Invoice
        </h1>
        <hr/>

        <div class="invoice-step" v-if="invoiceStep === 1">
            <h2>Step 1: Select Customer</h2>
            <div v-if="customers.length" class="invoice-step-detail">
                <label for="customer">Customer:</label>
                <select
                        v-model="selectedCustomerId"
                        class="invoice-customers"
                        id="customer"
                >
                    <option disabled value="">Please select a Customer</option>
                    <option v-for="customer in customers" :value="customer.id" :key="customer.id">
                        {{customer.firstName + " " + customer.lastName }}
                    </option>
                </select>
            </div>
        </div>

        <div class="invoice-step" v-if="invoiceStep === 2">
            <h2>Step 2: Create Order</h2>
        </div>

        <div class="invoice-step" v-if="invoiceStep === 3">

        </div>
    </div>
</template>


<script lang="ts">
    import {Component, Vue} from 'vue-property-decorator'
    import {IInvoice, ILineItem} from "@/types/Invoice";
    import {IProductInventory} from "@/types/Product";
    import {ICustomer} from "@/types/Customer";
    import CustomerService from "@/services/customer-service";
    import {InventoryService} from "@/services/inventory-service";
    import InvoiceService from "@/services/invoice-service";


    const customerService = new CustomerService();
    const inventoryService = new InventoryService();
    const invoiceService = new InvoiceService();

    @Component({name: 'Create Invoice'})
    export default class extends Vue {
        invoiceStep: number = 1;
        invoice: IInvoice = {
            createdOn: new Date(),
            customerId: 0,
            lineItems: [],
            updatedOn: new Date()
        };

        customers: ICustomer[] = [];
        selectedCustomerId: number = 0;
        inventory: IProductInventory[] = [];
        lineItems: ILineItem[] = [];
        newItem: ILineItem = {product: undefined, quantity: 0};

        async initialize(): Promise<void> {
            customerService.getCustomers().then(x => this.customers = x);
            inventoryService.getInventory().then(x => this.inventory = x);

            // this.customers = await customerService.getCustomers();
            // this.inventory = await inventoryService.getInventory();
        }

        async created() {
            await this.initialize();
            {
            }
        }
    }
</script>

<style lang="scss" scoped>
    @import "@/scss/global.scss";

    .invoice-steps-actions {
        display: flex;
        width: 100%;
    }

    .invoice-step {
    }

    .invoice-step-detail {
        margin: 1.2rem;
    }

    .invoice-order-list {
        margin-top: 1.2rem;
        padding: 0.8rem;

        .line-item {
            display: flex;
            border-bottom: 1px dashed #ccc;
            padding: 0.8rem;
        }

        .item-col {
            flex-grow: 1;
        }
    }

    .invoice-item-actions {
        display: flex;
    }

    .price-pre-tax {
        font-weight: bold;
    }

    .price-final {
        font-weight: bold;
        color: $solar-green;
    }

    .due {
        font-weight: bold;
    }

    .invoice-header {
        width: 100%;
        margin-bottom: 1.2rem;
    }

    .invoice-logo {
        padding-top: 1.4rem;
        text-align: center;

        img {
            width: 280px;
        }
    }
</style>