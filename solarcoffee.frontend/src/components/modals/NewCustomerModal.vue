<!--suppress XmlUnboundNsPrefix -->

<template>
    <solar-modal>
        <template v-slot:header>
            Add New Customer
        </template>
        <template v-slot:body>
            <ul class="newCustomer">
                <li>
                    <label for="firstName">First Name</label>
                    <input id="firstName" type="text" v-model="customer.firstName"/>
                </li>
                <li>
                    <label for="lastName">Last Name</label>
                    <input id="lastName" type="text" v-model="customer.lastName"/>
                </li>
                <li>
                    <label for="address1">Address Line 1</label>
                    <input
                            id="address1"
                            type="text"
                            v-model="customer.primaryAddress.addressLine1"
                    />
                </li>
                <li>
                    <label for="address2">Address Line 2</label>
                    <input
                            id="address2"
                            type="text"
                            v-model="customer.primaryAddress.addressLine2"
                    />
                </li>
                <li>
                    <label for="city">City</label>
                    <input id="city" type="text" v-model="customer.primaryAddress.city"/>
                </li>
                <li>
                    <label for="state">County</label>
                    <input
                            id="state"
                            type="text"
                            v-model="customer.primaryAddress.county"
                    />
                </li>
                <li>
                    <label for="postalCode">Postal Code</label>
                    <input
                            id="postalCode"
                            type="text"
                            v-model="customer.primaryAddress.postalCode"
                    />
                </li>
                <li>
                    <label for="country">Country</label>
                    <input
                            id="country"
                            type="text"
                            v-model="customer.primaryAddress.country"
                    />
                </li>
            </ul>
        </template>
        <template v-slot:footer>
            <solar-button
                    @button:click="save"
                    aria-label="save new customer"
                    type="button"
            >
                Save Customer
            </solar-button>

            <solar-button
                    @button:click="close"
                    aria-label="Close modal"
                    type="button"
            >
                Close
            </solar-button>
        </template>
    </solar-modal>
</template>

<script lang="ts">
    import {Component, Vue} from "vue-property-decorator";
    import SolarButton from "@/components/SolarButton.vue";
    import SolarModal from "@/components/modals/SolarModal.vue";
    import {ICustomer} from "@/types/Customer";

    @Component({
        name: "NewCustomerModal",
        components: {SolarButton, SolarModal}
    })
    export default class NewCustomerModal extends Vue {
        customer: ICustomer = {
            primaryAddress: {
                addressLine1: "",
                addressLine2: "",
                county:"",
                country: "",
                postalCode: "",
                city:"",
                createdOn: new Date(),
                updatedOn: new Date(),
            },
            createdOn: new Date(),
            updatedOn: new Date(),
            firstName: "",
            lastName: ""
        };

        save() {
            this.$emit("save:customer", this.customer);
        }

        close() {
            this.$emit("close");
        }
    }
</script>

<style lang="scss" scoped>
    .newCustomer {
        display: flex;
        flex-wrap: wrap;
        list-style: none;
        padding: 0;
        margin: 0;

        input {
            width: 80%;
            height: 1.8rem;
            margin: 0.8rem 2rem 0.8rem 0.8rem;
            font-size: 1.1rem;
            line-height: 1.3rem;
            padding: 0.2rem;
            color: #555;
        }

        label {
            font-weight: bold;
            margin: 0.8rem;
            display: block;
        }
    }
</style>