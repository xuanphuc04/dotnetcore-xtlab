let inputArr = [
  {
    optionTypeId: 4,
    name: "Size",
    options: [
      {
        productOptionId: 24,
        name: "M",
        value: "M",
      },
      {
        productOptionId: 25,
        name: "L",
        value: "L",
      },
      {
        productOptionId: 26,
        name: "XL",
        value: "XL",
      },
    ],
  },
  {
    optionTypeId: 5,
    name: "Color",
    options: [
      {
        productOptionId: 7,
        name: "Đen",
        value: "#000000",
      },
      {
        productOptionId: 8,
        name: "Trắng",
        value: "#ffffff",
      },
    ],
  },
];

// Đầu vào là mảng các productOptionId: [7, 24, 25, 26]
// ==> Đầu ra mong muốn:

// {
//     "Color": [
//         {
//             "productOptionId": 7,
//             "name": "Đen",
//             "value": "#000000"
//         }
//     ],
//     "Size": [
//         {
//             "productOptionId": 24,
//             "name": "M",
//             "value": "M"
//         },
//         {
//             "productOptionId": 25,
//             "name": "L",
//             "value": "L"
//         },
//         {
//             "productOptionId": 26,
//             "name": "XL",
//             "value": "XL"
//         }
//     ]
// }

let inputids = [7, 24, 25, 26];
console.log(inputArr);

let obj = {};
for (let i = 0; i < inputArr.length; i++) {
  for (let j = 0; j < inputArr[i].options.length; j++) {
    if (inputids.includes(inputArr[i].options[j].productOptionId)) {
      if (!Array.isArray(obj[inputArr[i].name])) {
        obj[inputArr[i].name] = [];
      }
      obj[inputArr[i].name].push(inputArr[i].options[j]);
    }
  }
}
console.log(obj);
