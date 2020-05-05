<template>
    <div class="inventory-container">
        <h1 id="inventoryTitle">Inventory Dashboard</h1>
        <hr/>

        <solar-button @click.native="showNewProductModal" id="addNewBtn">
            Add New Item
        </solar-button>
        <solar-button @click.native="showShipmentModal" id="receiveShipmentBtn">
            Receive Shipment
        </solar-button>

        <table class="table" id="inventoryTable">
            <tr>
                <th>Item</th>
                <th>Quantity On-hand</th>
                <th>Unit Price</th>
                <th>Taxable</th>
                <th>Delete</th>
            </tr>
            <tr :key="item.id" v-for="item in inventory">
                <td>{{ item.product.name }}</td>
                <td>{{ item.quantityOnHand }}</td>
                <td>{{ item.product.price | price }}</td>
                <td>{{ item.product.isTaxable ? "Yes" : "No" }}</td>
                <td>
                    <div>x</div>
                </td>
            </tr>
        </table>

        <new-product-modal
                @close="closeModals"
                @save:product="saveNewProduct"
                v-if="isNewProductVisible"
        />
        <shipment-modal
                :inventory="inventory"
                @close="closeModals"
                @save:shipment="saveNewShipment"
                v-if="isShipmentVisible"
        />
    </div>
</template>

<script lang="ts">
    import {Component, Vue} from "vue-property-decorator";
    import {IProduct, IProductInventory} from "@/types/Product";
    import SolarButton from "@/components/SolarButton.vue";
    import ShipmentModal from "@/components/modals/ShipmentModal.vue";
    import NewProductModal from "@/components/modals/NewProductModal.vue";
    import {IShipment} from "@/types/Shipment";

    @Component({
        name: "Inventory",
        components: {SolarButton, ShipmentModal, NewProductModal}
    })
    export default class Inventory extends Vue {
        isNewProductVisible: boolean = false;
        isShipmentVisible: boolean = false;

        inventory: IProductInventory[] = [
            {
                id: 1,
                product: {
                    id: 1,
                    name: "Some Product",
                    description: "Good stuff",
                    price: 100,
                    createdOn: new Date(),
                    updatedOn: new Date(),
                    isTaxable: true,
                    isArchived: false
                },
                quantityOnHand: 1,
                idealQuantity: 10
            },
            {
                id: 2,
                product: {
                    id: 2,
                    name: "Some other Product",
                    description: "Better stuff",
                    price: 190,
                    createdOn: new Date(),
                    updatedOn: new Date(),
                    isTaxable: false,
                    isArchived: false
                },
                quantityOnHand: 1,
                idealQuantity: 10
            }
        ];

        closeModals() {
            this.isNewProductVisible = false;
            this.isShipmentVisible = false;
        }

        showNewProductModal() {
            this.isNewProductVisible = true;
        }

        showShipmentModal() {
            this.isShipmentVisible= true;
        }

        saveNewProduct(product: IProduct) {
            console.log('saveNewProduct');
            console.log(product);
        }

        saveNewShipment(shipment: IShipment) {
            console.log('saveNewShipment');
            console.log(shipment);

        }
    }
</script>

<style lang="scss" scoped>
    @import "@/scss/global.scss";

    .green {
        font-weight: bold;
        color: $solar-green;
    }

    .yellow {
        font-weight: bold;
        color: $solar-yellow;
    }

    .red {
        font-weight: bold;
        color: $solar-red;
    }

    .inventory-actions {
        display: flex;
        margin-bottom: 0.8rem;
    }

    .product-archive {
        cursor: pointer;
        font-weight: bold;
        font-size: 1.2rem;
        color: $solar-red;
    }
</style>
