var app = new Vue({
    el: '.VueModel',

    data: {
        Products: [
            {
                id: '',
                name: '',
                Category: '',
                price: '',
                Tagnames: '',
                Index: '',
                Brand: '',
                Description: '',
                salePercentage: '',
                ProductInfos: [
                    {
                        Color: '',
                        Sizes: [],
                        Thumbnail: '',
                        StocksAndSizes: [{
                            Size: '',
                            Stock: '',
                        }],
                        stock: [],
                        dropzoneFiles: [],
                        dropzoneFilesUrl: [],
                        Index: '',
                        IndexInfo: '',
                    }
                ],
            }
        ]
    },

    methods: {
        sendToServer: function () {
            let formdata = new FormData()

            formdata.append("jsonProducts", JSON.stringify(this.Products))
            for (let i = 0; i < this.Products.length; i++) {
                for (let j = 0; j < this.Products[i].ProductInfos.length; j++) {
                    formdata.append("RoomImagesVms[" + i + "].ProductlIndex", i)
                    formdata.append("RoomImagesVms[" + i + "].ThumbnailIndex", this.ProductInfos[i].ProductInfos[j].Thumbnail)
                    for (let g = 0; g < this.Products[i].ProductInfos[j].dropzoneFiles.length; g++) {
                        formdata.append("RoomImagesVms[" + i + "].RoomImages", this.ProductInfos[i].ProductInfos[j].dropzoneFiles[g])
                    }

                }
            }

            axios({
                method: 'post',
                url: '/Panel/AddProductPanel',
                data: formdata

            })
                .then(function (response) {
                    alert("product was added");
                })
                .catch(function (error) {
                    console.log(error);
                });

        },
        drop(e, ProdInfoIndex, ProdDetailsIndex) {
            console.log(e)


            this.Products[ProdInfoIndex]
                .ProductInfos[ProdDetailsIndex].
                dropzoneFiles.push(e.dataTransfer.files[0])

            let url = URL.createObjectURL(e.dataTransfer.files[0])
            this.Products[ProdInfoIndex]
                .ProductInfos[ProdDetailsIndex]
                .dropzoneFilesUrl.push(url)

        },
        SelectFiles(ProdInfoIndex, ProdDetailsIndex, e) {
            console.log(e)
            console.log(ProdInfoIndex, ProdDetailsIndex)

            for (let i = 0; i < e.target.files.length; i++) {

                this.Products[ProdInfoIndex].ProductInfos[ProdDetailsIndex].
                    dropzoneFiles.push(e.target.files[i])

                let url = URL.createObjectURL(e.target.files[i])
                this.Products[ProdInfoIndex].ProductInfos[ProdDetailsIndex].
                    dropzoneFilesUrl.push(url)

            }


        },

        AddNewProductInfo: function () {
            this.Products.push({

                name: '',
                Index: '',
                Category: '',
                price: '',
                Tagnames: '',
                Brand: '',
                Description: '',
                ProductInfos: [
                    {
                        Color: '',

                        Sizes: [],
                        stock: [],
                        dropzoneFiles: [],
                        active: false,
                        dropzoneFilesUrl: [],

                        Index: '',
                        IndexInfo: '',



                    }]
                ,


            })
        },
        PopulateStocks: function (ProdInfoIndex, ProdDetailsIndex, event) {

            console.log(event)
            console.log(ProdInfoIndex, ProdDetailsIndex)
            if (event.target.checked) {
                this.Products[ProdInfoIndex]
                    .ProductInfos[ProdDetailsIndex]
                    .stock.push({ SizeName: event.target.value, number: '' })
            }
            else {




                for (let i = 0; i < this.ProductInfos[ProdInfoIndex]
                    .ProductInfos[ProdDetailsIndex].stock.length; i++) {
                    if (this.Products[ProdInfoIndex]
                        .ProductInfos[ProdDetailsIndex].stock[i].SizeName == event.target.value) {
                        this.Products[ProdInfoIndex]
                            .ProductInfos[ProdDetailsIndex].stock.splice(i, 1)
                    }

                }




            }



        },
        AddNewProductDetail: function (Index) {


            this.Products[Index].ProductInfos.push({
                Color: '',
                Sizes: [],
                stock: [],
                dropzoneFiles: [],

                dropzoneFilesUrl: [],

                Index: '',
                IndexInfo: '',


            })
        },
        RemoveImage(e, index, ProdInfoIndex, ProdDetailsIndex) {
            this.Products[ProdInfoIndex].ProductInfos[ProdDetailsIndex].
                dropzoneFiles.splice(index, 1)
            this.Products[ProdInfoIndex].ProductInfos[ProdDetailsIndex].
                dropzoneFilesUrl.splice(index, 1)
            console.log(e)
        },

        RemoveProductInfo(Index) {
            this.ProductInfos.splice(Index, 1)
        },
        RemoveProductDetail(DetailIndex, InfoIndex) {
            console.log()
            this.Products[InfoIndex].ProductInfos.splice(DetailIndex, 1)
        },

        toggleActive(ProdInfoIndex, ProdDetailIndex) {
            this.Products[ProdInfoIndex].ProductInfos[ProdDetailIndex].active =
                !this.Products[ProdInfoIndex].ProductInfos[ProdDetailIndex].active
        }
    }

})