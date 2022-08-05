
Vue.component('dropzonec', {
    props: ['index','indexx'],
    template: `

               
                   <div 
                               @dragenter.prevent="toggleActive"
                                @dragleave.prevent="toggleActive" 
                               @dragover.prevent
                               @drop.prevent="toggleActive"
                                 :class="{'active-dropzone':this.active}"
                                   class="DropzoneC">
                                  <span>Drag And Drop File</span>
                              <span>Or</span>
                                 <label for="DropzoneFile">Select File</label>
                                <input type="file" id="DropzoneFile" class="selectFiles" multiple />
                                
                              

                                     </div>
                        `,
    data: function () {
      
        return {
            active: false,
               }
    },
    methods: {

        toggleActive: function () {
           
          this.active = !this.active
            
        },
        

    }
    


   

    });
      


